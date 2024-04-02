using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using csharp_project.model;
using csharp_project.service;

namespace csharp_project
{
    public partial class AccountForm : Form
    {
        private static IService _service;
        private static Employee _currentEmployee;
        
        public AccountForm(IService service, Employee currentEmployee)
        {
            InitializeComponent();
            _service = service;
            _currentEmployee = currentEmployee;
        }
        
        private void LoadDataGridViews(object sender, EventArgs e)
        {
            dataGridViewEvents.AutoGenerateColumns = true;
            dataGridViewEvents.DataSource = _service.GetAllAgeEvents();
            dataGridViewEvents.Columns.Remove("Id");
            
            dataGridViewEvents.Columns.Add("ageGroup", "Age Group");
            dataGridViewEvents.Columns.Add("sportsEvent", "Sports Event");
            dataGridViewEvents.Columns.Add("noParticipants", "No. Participants");
            
            foreach (DataGridViewRow row in dataGridViewEvents.Rows)
            {
                if (row.DataBoundItem is AgeEvent ageEvent)
                {
                    row.Cells["ageGroup"].Value = ageEvent.GetAgeGroup().ToString();
                    row.Cells["sportsEvent"].Value = ageEvent.GetSportsEvent().ToString();
                    var noParticipants = CountParticipants(ageEvent);
                    row.Cells["noParticipants"].Value = noParticipants;
                }
            }
            
            dataGridViewParticipants.AutoGenerateColumns = true;
            dataGridViewParticipants.DataSource = _service.GetAllParticipants();
            dataGridViewParticipants.Columns.Remove("Id");
            
            dataGridViewParticipants.Columns.Add("noRegistrations", "No. Registrations");
            
            foreach (DataGridViewRow row in dataGridViewParticipants.Rows)
            {
                if (row.DataBoundItem is Participant participant)
                {
                    var noRegistrations = CountRegistrations(participant);
                    row.Cells["noRegistrations"].Value = noRegistrations;
                }
            }
        }
        
        private static int CountParticipants(AgeEvent ageEvent)
        {
            var registrations = _service.FindByAgeEvent(ageEvent);
            var participantsCountMap = new Dictionary<long, int>();

            foreach (var registration in registrations)
            {
                var eventId = registration.GetAgeEvent().Id;
                if (participantsCountMap.ContainsKey(eventId))
                {
                    participantsCountMap[eventId]++;
                }
                else
                {
                    participantsCountMap[eventId] = 1;
                }
            }

            return participantsCountMap.ContainsKey(ageEvent.Id) ? participantsCountMap[ageEvent.Id] : 0;
        }
        
        private static int CountRegistrations(Participant participant)
        {
            var registrations = _service.FindByParticipant(participant);
            return registrations.Count;
        }

        private void HandleSearch(object sender, EventArgs e)
        {
            try
            {
                var selectedRow = dataGridViewEvents.SelectedRows[0];
                var ageGroup = selectedRow.Cells["ageGroup"].Value;
                var sportsEvent = selectedRow.Cells["sportsEvent"].Value;
                var ageEvent = _service.FindByAgeGroupAndSportsEvent(ageGroup.ToString(), sportsEvent.ToString());
                
                var searchForm = new SearchForm(_service, ageEvent);
                searchForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while showing search dialog: " + ex.Message);
            }
        }

        private void HandleRegister(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewParticipants.SelectedRows.Count == 0)
                {
                    var registerForm = new RegisterForm(_service, _currentEmployee, null);
                    registerForm.ShowDialog();   
                }
                else
                {
                    var selectedRow = dataGridViewParticipants.SelectedRows[0];
                    var firstName = selectedRow.Cells["firstName"].Value;
                    var lastName = selectedRow.Cells["lastName"].Value;
                    var age = selectedRow.Cells["age"].Value;
                    var participant = _service.FindOneByNameAndAge(firstName.ToString(), lastName.ToString(), Convert.ToInt32(age));    
                    
                    if (_service.FindByParticipant(participant).Count == 2)
                        MessageAlert.ShowErrorMessage(null, "This participant is already registered in the maximum number of events (2)!");
                    else
                    {
                        var registerForm = new RegisterForm(_service, _currentEmployee, participant);
                        registerForm.ShowDialog();   
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while showing register dialog: " + ex.Message);
            }
        }
    }
}
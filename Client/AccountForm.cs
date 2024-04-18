using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Client;
using csharp_project.model;
using csharp_project.service;
using Services.dto;

namespace csharp_project
{
    public partial class AccountForm : Form, IObserver
    {
        /*public event EventHandler<UserEventArgs> UpdateEvent;*/
        private static IService _service;
        private static Employee _currentEmployee;
        
        public AccountForm(IService service, Employee currentEmployee)
        {
            InitializeComponent();
            _service = service;
            _currentEmployee = currentEmployee;
            
            /*UpdateEvent += UserUpdate;*/
        }

        public void SetConnectedEmployee(Employee connectedEmployee)
        {
            _currentEmployee = connectedEmployee;
        }

        /*private delegate void UpdateDataGridViewCallback(DataGridView dataGridView, IList<object> data);
        
        private static void UpdateDataGridView(DataGridView dataGridView, IList<object> newData)
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = newData;
        }

        public void UserUpdate(object sender, UserEventArgs e)
        {
            if (e.Type() == UserEvent.NewParticipant)
            {
                var ageEvents = _service.GetAllAgeEvents();
                var ageEventsDto = new ArrayList();
                foreach (var ageEvent in ageEvents)
                {
                    var ageEventDto = DtoUtils.GetDto(ageEvent);
                    ageEventDto.SetNoParticipants(_service.CountParticipants(ageEvent));
                    ageEventsDto.Add(ageEventDto);
                }
                
                dataGridViewEvents.BeginInvoke(new UpdateDataGridViewCallback(UpdateDataGridView),
                    new object[] { dataGridViewEvents, ageEventsDto });
                
                var participants = _service.GetAllParticipants();
                var participantsDto = new ArrayList();
                foreach (var participant in participants)
                {
                    var participantDto = DtoUtils.GetDto(participant);
                    participantDto.SetNoRegistrations(_service.CountRegistrations(participant));
                    participantsDto.Add(participantDto);
                }

                dataGridViewParticipants.BeginInvoke(new UpdateDataGridViewCallback(UpdateDataGridView),
                    new object[] { dataGridViewParticipants, participantsDto });
            }
        }*/
        
        private void LoadDataGridViews(object sender, EventArgs e)
        {
            var ageEvents = _service.GetAllAgeEvents();
            var ageEventsDto = new ArrayList();
            foreach (var ageEvent in ageEvents)
            {
                var ageEventDto = DtoUtils.GetDto(ageEvent);
                ageEventDto.SetNoParticipants(_service.CountParticipants(ageEvent));
                ageEventsDto.Add(ageEventDto);
            }
            
            dataGridViewEvents.AutoGenerateColumns = false;
            dataGridViewEvents.DataSource = ageEventsDto;
            
            dataGridViewEvents.Columns.Add("AgeGroup", "Age Group");
            dataGridViewEvents.Columns.Add("SportsEvent", "Sports Event");
            dataGridViewEvents.Columns.Add("NoParticipants", "No. Participants");
            
            foreach (DataGridViewRow row in dataGridViewEvents.Rows)
            {
                if (row.DataBoundItem is AgeEventDto ageEvent)
                {
                    row.Cells["AgeGroup"].Value = ageEvent.GetAgeGroup().ToString();
                    row.Cells["SportsEvent"].Value = ageEvent.GetSportsEvent().ToString();
                    row.Cells["NoParticipants"].Value = ageEvent.GetNoParticipants().ToString();
                }
            }
            
            var participants = _service.GetAllParticipants();
            var participantsDto = new ArrayList();
            foreach (var participant in participants)
            {
                var participantDto = DtoUtils.GetDto(participant);
                participantDto.SetNoRegistrations(_service.CountRegistrations(participant));
                participantsDto.Add(participantDto);
            }
            
            dataGridViewParticipants.AutoGenerateColumns = false;
            dataGridViewParticipants.DataSource = participantsDto;
            
            dataGridViewParticipants.Columns.Add("Name", "Full Name");
            dataGridViewParticipants.Columns.Add("Age", "Age");
            dataGridViewParticipants.Columns.Add("NoRegistrations", "No. Registrations");
            
            foreach (DataGridViewRow row in dataGridViewParticipants.Rows)
            {
                if (row.DataBoundItem is ParticipantDto participant)
                {
                    row.Cells["Name"].Value = participant.GetName();
                    row.Cells["Age"].Value = participant.GetAge().ToString();
                    row.Cells["NoRegistrations"].Value = participant.GetNoRegistrations().ToString();
                }
            }
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
                    var name = selectedRow.Cells["name"].Value;
                    var age = selectedRow.Cells["age"].Value;
                    var participant = _service.FindOneByNameAndAge(name.ToString().Split(' ')[0], name.ToString().Split(' ')[1], Convert.ToInt32(age));    
                    
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

        public void NotifyAddRegistration(Registration registration)
        {
            Console.WriteLine("NotifyAddRegistration");
            
            BeginInvoke((MethodInvoker) delegate
            {
                try
                {
                    dataGridViewEvents.Columns.Remove("AgeGroup");
                    dataGridViewEvents.Columns.Remove("SportsEvent");
                    dataGridViewEvents.Columns.Remove("NoParticipants");
                    
                    dataGridViewParticipants.Columns.Remove("Name");
                    dataGridViewParticipants.Columns.Remove("Age");
                    dataGridViewParticipants.Columns.Remove("NoRegistrations");
                    
                    LoadDataGridViews(null, null);
                }
                catch (Exception e)
                {
                    throw new ApplicationException(e.Message, e);
                }
            });
        }

        /*public void NotifyAddParticipant(Participant participant)
        {
            Console.WriteLine("NotifyAddParticipant");
            
            BeginInvoke((MethodInvoker) delegate
            {
                try
                {
                    dataGridViewEvents.Columns.Remove("AgeGroup");
                    dataGridViewEvents.Columns.Remove("SportsEvent");
                    dataGridViewEvents.Columns.Remove("NoParticipants");
                    
                    dataGridViewParticipants.Columns.Remove("Name");
                    dataGridViewParticipants.Columns.Remove("Age");
                    dataGridViewParticipants.Columns.Remove("NoRegistrations");
                    
                    LoadDataGridViews(null, null);
                }
                catch (Exception e)
                {
                    throw new ApplicationException(e.Message, e);
                }
            });
        }*/

        /*protected virtual void OnUserEvent(UserEventArgs e)
        {
            if (UpdateEvent == null) return;
            Console.WriteLine("OnUserEvent: " + UpdateEvent + " - " + e.Type() + " - " + e.Data());
            UpdateEvent(this, e);
        }*/

        private void HandleLogOut(object sender, EventArgs e)
        {
            try
            {
                _service.Logout(_currentEmployee.Id);
                /*UpdateEvent -= UserUpdate;*/
                Application.Exit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("LogOut error " + ex.Message);
            }
        }
    }
}
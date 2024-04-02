using System;
using System.Collections;
using System.Windows.Forms;
using csharp_project.model;
using csharp_project.service;

namespace csharp_project
{
    public partial class SearchForm : Form
    {
        private static IService _service;
        private static AgeEvent _currentAgeEvent;
        
        public SearchForm(IService service, AgeEvent currentAgeEvent)
        {
            InitializeComponent();
            _service = service;
            _currentAgeEvent = currentAgeEvent;
        }
        
        private void LoadDataGridView(object sender, EventArgs e)
        {
            dataGridViewParticipants.AutoGenerateColumns = true;

            var participants = new ArrayList();
            foreach (var registration in _service.FindByAgeEvent(_currentAgeEvent))
            {
                participants.Add(registration.GetParticipant());
            }
            
            dataGridViewParticipants.DataSource = participants;
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
        
        private static int CountRegistrations(Participant participant)
        {
            var registrations = _service.FindByParticipant(participant);
            return registrations.Count;
        }
    }
}
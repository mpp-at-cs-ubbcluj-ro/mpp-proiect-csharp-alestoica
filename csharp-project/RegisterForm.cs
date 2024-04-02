using System;
using System.Collections;
using System.Windows.Forms;
using csharp_project.model;
using csharp_project.service;

namespace csharp_project
{
    public partial class RegisterForm : Form
    {
        private static IService _service;
        private static Employee _currentEmployee;
        private static Participant _currentParticipant;
        
        public RegisterForm(IService service, Employee currentEmployee, Participant currentParticipant)
        {
            InitializeComponent();
            _service = service;
            _currentEmployee = currentEmployee;
            _currentParticipant = currentParticipant;
        }

        public void LoadData(object sender, EventArgs e)
        {
            var ages = new ArrayList();
            
            for (var i = 6; i <= 15; i++)
            {
                ages.Add(i);
            }
            
            comboBoxAge.Items.AddRange(ages.ToArray());

            if (_currentParticipant != null)
            {
                textBoxFirstName.Text = _currentParticipant.FirstName;
                textBoxLastName.Text = _currentParticipant.LastName;
                comboBoxAge.SelectedIndex = _currentParticipant.Age - 6;
                comboBoxAge.Enabled = false;

                var registeredEvents = new ArrayList();
                foreach (var registration in _service.FindByParticipant(_currentParticipant))
                {
                    registeredEvents.Add(registration.GetAgeEvent().GetSportsEvent().ToString());
                }

                var events = new ArrayList();
                
                if (_currentParticipant.Age >= 6 && _currentParticipant.Age <= 8)
                {
                    events.Add("Meters50");
                    events.Add("Meters100");
                }
                else if (_currentParticipant.Age >= 9 && _currentParticipant.Age <= 11)
                {
                    events.Add("Meters100");
                    events.Add("Meters1000");
                }
                else if (_currentParticipant.Age >= 12 && _currentParticipant.Age <= 15)
                {
                    events.Add("Meters1000");
                    events.Add("Meters1500");
                }
                
                foreach (var registeredEvent in registeredEvents)
                {
                    events.Remove(registeredEvent);
                }

                Console.WriteLine();
                comboBoxEvent.Items.AddRange(events.ToArray());
                comboBoxEvent.SelectedIndex = 0;
            }
            else
            {
                comboBoxAge.SelectedIndex = 0;
                
                comboBoxAge.SelectedIndexChanged += (sender1, e1) =>
                {
                    var age = (int)comboBoxAge.SelectedItem;
                    
                    var events = new ArrayList();
                    
                    if (age >= 6 && age <= 8)
                    {
                        events.Add("Meters50");
                        events.Add("Meters100");
                    }
                    else if (age >= 9 && age <= 11)
                    {
                        events.Add("Meters100");
                        events.Add("Meters1000");
                    }
                    else if (age >= 12 && age <= 15)
                    {
                        events.Add("Meters1000");
                        events.Add("Meters1500");
                    }
                    
                    comboBoxEvent.Items.Clear();
                    comboBoxEvent.Items.AddRange(events.ToArray());
                    comboBoxEvent.SelectedIndex = 0; 
                };
                
                comboBoxAge.Select();
            }
        }

        private void HandleRegister(object sender, EventArgs e)
        {
            if (_currentParticipant == null)
            {
                var random = new Random();
                var id = random.Next(100000);
                var firstName = textBoxFirstName.Text;
                var lastName = textBoxLastName.Text;
                var age = Convert.ToInt32(comboBoxAge.SelectedItem);

                var participant = _service.FindOneByNameAndAge(firstName, lastName, age);

                if (participant == null)
                {
                    participant = new Participant(id, firstName, lastName, age);

                    _currentParticipant = participant;

                    _service.AddParticipant(participant);
                    Console.WriteLine("Participant successfully added!");
                }
                else
                {
                    MessageAlert.ShowErrorMessage(null, "This participant already exists and you have to select it from the previous page!");
                }

                var sportsEvent = comboBoxEvent.SelectedItem;
                var ageGroup = "Group68Years";
                
                if (age >= 9 && age <= 11)
                {
                    ageGroup = "Group911Years";
                }
                else if (age >= 12 && age <= 15)
                {
                    ageGroup = "Group1215Years";
                }

                var ageEvent = _service.FindByAgeGroupAndSportsEvent(ageGroup, sportsEvent.ToString());
                
                var idRegistration = random.Next(100000);
                var registration = new Registration(idRegistration, participant, ageEvent, _currentEmployee);
                _service.AddRegistration(registration);
                Console.WriteLine("Registration successfully added!");
            }
            else
            {
                var sportsEvent = comboBoxEvent.SelectedItem;
                var age = Convert.ToInt32(comboBoxAge.SelectedItem);
                var ageGroup = "Group68Years";
                
                if (age >= 9 && age <= 11)
                {
                    ageGroup = "Group911Years";
                }
                else if (age >= 12 && age <= 15)
                {
                    ageGroup = "Group1215Years";
                }

                var ageEvent = _service.FindByAgeGroupAndSportsEvent(ageGroup, sportsEvent.ToString());

                var random = new Random();
                var idRegistration = random.Next(100000);
                var registration = new Registration(idRegistration, _currentParticipant, ageEvent, _currentEmployee);
                _service.AddRegistration(registration);
                Console.WriteLine("Registration successfully added!");
            }
        }
    }
}
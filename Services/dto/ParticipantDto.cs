using System.IO;

namespace Services.dto
{
    public class ParticipantDto
    {
        private long _id;
        private string _name;
        private int _age;
        private int _noRegistrations;

        public ParticipantDto(long id, string name, int age)
        {
            _id = id;
            _name = name;
            _age = age;
        }
        
        public long GetId()
        {
            return _id;
        }
        
        public int GetAge()
        {
            return _age;
        }

        public string GetName()
        {
            return _name;
        }
        
        public int GetNoRegistrations()
        {
            return _noRegistrations;
        }
        
        public void SetId(long id)
        {
            _id = id;
        }
        
        public void SetAge(int age)
        {
            _age = age;
        }

        public void SetName(string name)
        {
            _name = name;
        }
        
        public void SetNoRegistrations(int noRegistrations)
        {
            _noRegistrations = noRegistrations;
        }
    }
}
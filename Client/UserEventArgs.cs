using System;

namespace Client
{
    public class UserEventArgs : EventArgs
    {
        private readonly UserEvent _userEvent;
        private readonly object _data;

        public UserEventArgs(UserEvent userEvent, object data)
        {
            _userEvent = userEvent;
            _data = data;
        }
        
        public UserEvent Type()
        {
            return _userEvent;
        }
        
        public object Data()
        {
            return _data;
        }
    }
}
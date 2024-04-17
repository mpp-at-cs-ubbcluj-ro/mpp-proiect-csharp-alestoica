using System;
using Networking.rpcProtocol;

namespace Networking.rpcProtocol
{
    [Serializable]
    public class Request
    {
        private Request() {}
        
        private RequestType Type { get; set; }
        private object Data { get; set; }

        public RequestType GetType()
        {
            return Type;
        }

        public object GetData()
        {
            return Data;
        }

        private void SetData(object data)
        {
            Data = data;
        }

        private void SetType(RequestType type)
        {
            Type = type;
        }

        public override string ToString()
        {
            return "Request: type: " + Type + ", data: " + Data;
        }

        public class Builder
        {
            private readonly Request _request = new Request();

            public Builder Type(RequestType type)
            {
                _request.SetType(type);
                return this;
            }

            public Builder Data(object data)
            {
                _request.SetData(data);
                return this;
            }

            public Request Build()
            {
                return _request;
            }
        }
    }
}
using System;

namespace Networking.rpcProtocol
{
    [Serializable]
    public class Response
    {
        private Response() {}
        
        private ResponseType Type { get; set; }
        private object Data { get; set; }

        public ResponseType GetType()
        {
            return Type;
        }

        public object GetData()
        {
            return Data;
        }

        private void SetType(ResponseType type)
        {
            Type = type;
        }

        private void SetData(object data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return "Response: type: " + Type + ", data: " + Data;
        }

        public class Builder
        {
            private readonly Response _response = new Response();

            public Builder Type(ResponseType type)
            {
                _response.SetType(type);
                return this;
            }

            public Builder Data(object data)
            {
                _response.SetData(data);
                return this;
            }

            public Response Build()
            {
                return _response;
            }
        }
    }
}
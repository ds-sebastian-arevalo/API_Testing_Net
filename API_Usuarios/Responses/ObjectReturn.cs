using API_Usuarios.Models;

namespace API_Usuarios.Responses
{
    public class ObjectReturn<T>
    {
        public T Data { get; set; }
        public string Info { get; set; }

        public ObjectReturn(T data, string info)
        {
            Data = data;
            Info = info;
        }
    }
}

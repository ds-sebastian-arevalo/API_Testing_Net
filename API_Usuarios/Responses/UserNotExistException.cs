namespace API_Usuarios.Responses
{
    public class UserNotExistException : Exception
    {
        public override string Message => "El Usuario no Existe!";
    }
}


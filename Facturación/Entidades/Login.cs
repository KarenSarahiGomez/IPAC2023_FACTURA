namespace Entidades
{
    public class Login
    {
        public string CodigoUsuario { get; set; }
        public string Contrasena { get; set; }

        public Login() //Siempre se debe crear 
        {
        }

        public Login(string codigoUsuario, string contrasena)
        {
            CodigoUsuario = codigoUsuario;
            Contrasena = contrasena;
        }
    }
}

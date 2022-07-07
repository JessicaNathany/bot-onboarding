namespace bot_onboarding.Bots
{
    public class InfoUsuario
    {
        public static string Nome;
        public static string Time;
        public static string Funcao;
        public static string Mensagem;
        public static TipoPapel TipoPapel;
        public static Acessos TipoAcesso;
    }

    public enum TipoPapel
    {
        Dev = 1,
        UX = 2,
        QA = 3
    }
    public enum Acessos
    {
        JiraCoFluence = 1,
        Gitlab = 2,
        BancoDados = 3,
        VPN = 4,
        AWS = 5,
        GruposTeams = 6,
        GSenha = 7,
        RequisitosMinimosAmbiente = 8
    }
}


namespace bot_onboarding.Bots
{
    public class InfoUsuario
    {
        public static string Nome;
        public static string Time;
        public static string Funcao;
        public static string Mensagem;
        public static TipoPapel TipoPapel;
        public static Acessos Acessos;
    }

    public enum TipoPapel
    {
        Dev = 1,
        UX = 2,
        QA = 3
    }
    public enum Acessos
    {
        AWS = 1,
        Gitlab = 2,
        BancoDados = 3,
        VPN = 4,
        JiraCoFluence = 5,
        GruposTeams = 6,
        SistemasAmbientes = 7,
        Zeplim = 8
    }
}


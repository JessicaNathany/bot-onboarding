namespace bot_onboarding.Bots
{
    public static class InformacaoUsuario
    {
        public static string nome;
        public static string time;
        public static string funcao;
        public static string mensagem;
        public static TipoPapel tipoPapel;
    }

    public enum TipoPapel
    {
        Dev = 1,
        UX = 2,
        QA = 3
    }
}


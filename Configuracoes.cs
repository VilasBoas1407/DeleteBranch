namespace DeleteBranchs
{
    public static class Configuracoes
    {
        public const string CAMINHO_PROJETO_FRONT = @"CAMINHO_DO_PROJETO";
        public const string CAMINHO_PROJETO_BACK = @"CAMINHO_DO_PROJETO";

        public const string GIT_FETCH = "git fetch";
        public const string GIT_DELETE = "git branch --delete";
        public const string GIT_BRANCH = "&& git branch -a --merged";
    }
}

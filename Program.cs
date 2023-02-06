using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DeleteBranchs
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            bool excluirBranch = false;
            string comando = @$"cd {Configuracoes.CAMINHO_PROJETO_BACK}";
            Console.WriteLine("Iniciando processo de limpeza...");

            Console.WriteLine("Acessando diretório: " + Configuracoes.CAMINHO_PROJETO_BACK);
            comando += Configuracoes.GIT_BRANCH;
            string saida = ExecutarCMD(comando);

            string [] branchs = saida.Split("\n");
           
            for (int i = 0; i < branchs.Length; i++)
            {
                string branchAtual = branchs[i];
                if (BranchsParaIgnorar(branchAtual.Trim()))
                    continue;

                Console.WriteLine("Deletando branch:" + branchAtual);
                ExecutarCMD($"{Configuracoes.GIT_DELETE} {branchAtual}");
                excluirBranch = true;
            }

            if (!excluirBranch)
                Console.WriteLine("Não existem branchs para excluir");

            Console.WriteLine("Atualizando repositório");
            ExecutarCMD(Configuracoes.GIT_FETCH);
        }

        public static bool BranchsParaIgnorar(string branchName)
        {
            if (branchName.Contains("develop") || branchName.Contains("remote") || branchName.Contains("*") || branchName == "")
                return true;
            else
                return false;
        }

        public static string ExecutarCMD(string comando)
        {
            using (Process processo = new Process())
            {
                processo.StartInfo.FileName = Environment.GetEnvironmentVariable("comspec");

                processo.StartInfo.Arguments = string.Format("/c {0}", comando);
                processo.StartInfo.RedirectStandardOutput = true;
                processo.StartInfo.UseShellExecute = false;
                processo.StartInfo.CreateNoWindow = true;

                processo.Start();
                processo.WaitForExit();
                
                string saida = processo.StandardOutput.ReadToEnd();
                return saida;
            }
        }
    }
}

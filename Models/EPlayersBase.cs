using System.Collections.Generic;
using System.IO;

namespace Eplayers_AspNetCore.Models
{
    public abstract class EPlayersBase
    {
        public void CreateFolderAndFile(string path)
        {
            // Database/Equipe.csv
            string folder = path.Split("/")[0];

            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if(!File.Exists(path))
            {
                
                File.Create(path);
            }

        }
            public List<string> ReadAllLinesCSV(string path)
            {
                List<string> linhas = new List<string>();

                // using -> abrir e fechar determinado tipo de arquivo ou conexão
                // StreamReader -> Ler as informações do meu csv

                using (StreamReader file = new StreamReader(path))
                {
                    string linha;
                    while ((linha = file.ReadLine()) != null)
                    {
                        linhas.Add(linha);
                    }
                }
                
                return linhas;
            }

            public void RewriteCSV(string path, List<string> linhas)
            {
                // StreamWriter -> Escrita de arquivos
                using (StreamWriter ouput = new StreamWriter(path))
                {
                    foreach (var item in linhas)
                    {
                        ouput.Write(item + '\n');
                    }
                }
            }
    }
}
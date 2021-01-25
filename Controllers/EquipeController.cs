using System;
using System.IO;
using Eplayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eplayers_AspNetCore.Controllers
{   
    // http://localhost:5001/Equipe/Listar
    [Route("Equipe")]
    public class EquipeController : Controller
    {
        // Criamos uma instância equipeModel com a estrutura Equipe
        Equipe equipeModel = new Equipe();

        [Route("Listar")]

        public IActionResult Index()
        {   
            // Listando todas as equipes e enviando para a view, através da ViewBag
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }
        
        // http://localhost:5001/Equipe/Cadastrar       
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            // Criamos uma nova instância de Equipe
            // e armazenamos os dados enviados pelo usuário
            // através do formulário
            // e salvamos no objeto novaEquipe
            Equipe novaEquipe   = new Equipe();
            novaEquipe.IdEquipe = int32.Parse(form["idjogador"])
            novaEquipe.Nome     = form["Nome"];
            
            
            // Upload Início
            // Verificamos se o usuário anexou um arquivo
            if ( form.Files.Count > 0 )
            {
                // Se sim,
                // Armazenamos o arquivo na variável file
                var file    = form.Files[0];
                var folder  = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot/img/Equipes" );

                // Verificamos e a pasta Equipes não existe
                if (!Directory.Exists(folder))
                {   
                    // Se não existe a pasta, a criamos
                    Directory.CreateDirectory(folder);
                }       
                                                 // localhost:5001 +                                + Equipes + equipe.jpg 
                var path = Path.Combine( Directory.GetCurrentDirectory(), "www.root/img/", folder, file.FileName );

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    // Salvamos o arquivo no caminho especificado
                    file.CopyTo(stream);  
                }           

                novaEquipe.Imagem = file.FileName;
            }
            else
            {
                novaEquipe.Imagem = "padrao.png";
            }
        
            // Upload Término 

            // Chamamos o método Create para salvar
            // a novaEquipe no CSV
            equipeModel.Create(novaEquipe);
            ViewBag.Equipes = equipeModel.ReadAll();
            
            return LocalRedirect("~/Equipe/Listar");

        }

        // http://localhost:5001/Equipe/1
        [Route("{id}")]
        public IActionResult Excluir(int id)
        {
            equipeModel.Delete(id);

            ViewBag.Equipes = equipeModel.ReadAll();
            
            return LocalRedirect("~/Equipe/Listar");
        }
    }
}
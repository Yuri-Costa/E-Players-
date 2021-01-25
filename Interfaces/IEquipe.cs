using System.Collections.Generic;
using Eplayers_AspNetCore.Models;

namespace Eplayers_AspNetCore.Interfaces
{
    public interface IEquipe
    {
         //CRUD
        void Create(Equipe e);
        List<Equipe> ReadAll();
        void Update(Equipe e);
        void Delete(int id);    


    }
}
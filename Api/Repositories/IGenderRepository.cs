using Api.Models;

namespace Api.Repositories
{
    public interface IGenderRepository
    {
      
            List<Gender> GetGenders();
            Gender GetGenderById(int id);
            void AddGender(Gender gender);
            void UpdateGender(Gender gender);
            string DeleteGender(int id);
        }
    }


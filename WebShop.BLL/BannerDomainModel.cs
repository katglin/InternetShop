using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL;
using WebShop.DAL.Repository;
using WebShop.Models;

namespace WebShop.BLL
{
    public class BannerDomainModel: BaseDomainModel
    {
        public IEnumerable<BannerModel> GetAll()
        {
            using (var repository = new BaseRepository<Image, int>())
            {
                var list = repository.Query().Select(x => new BannerModel { Image_id = x.Image_id, Image_path = x.Image_path, Artikul = x.Artikul }).ToList();
                return list;
            }
        }
        public void Create(BannerModel BM)
        {
            DAL.Image dalim = new DAL.Image();
            dalim.Artikul = BM.Artikul;
            dalim.Image_id = BM.Image_id;
            dalim.Image_path = BM.Image_path;
            using (var repository = new BaseRepository<DAL.Image, long>())
            {
                repository.Insert(dalim);
            }
        }

        public void Delete(long id)
        {
            using (var repository = new BaseRepository<DAL.Image, long>())
            {
                repository.Delete(id);
            }
        }
    }
}

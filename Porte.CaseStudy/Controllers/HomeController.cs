using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Porte.CaseStudy.Models;

namespace Porte.CaseStudy.Controllers
{
    public class HomeController : Controller
    {
        private readonly PorteDBContext _context;
        private readonly int partCost = 50;
        private readonly int partWeightCost = 7;
        private readonly int minumumPartCount = 2;

        public HomeController(PorteDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            SetPartCount();
            var model = new BoxViewModel()
            {
                Boxes = _context.Boxs.ToList(),
                Parts = _context.Parts.ToList(),
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult PartialParts()
        {

            var model = SetParts();

            return PartialView("_PartialParts", model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private int CalculatePartCost(int weight)//Verilen formüle göre parça maliyeti hesaplanıyor
        {
            return partCost + (weight * partWeightCost);
        }
        
        private void SetPartCount()//Paketlerin parça sayılarını hesaplanıyor ve ilgili tablo güncelleniyor.
        {
            var collection = _context.Boxs.ToList();
            int partCount = minumumPartCount;
            _context.BeginTransaction();
            foreach (var item in collection.OrderBy(m => m.WEIGHT))//Ürünleri hafiften ağıra doğru sıralanıyor.
            {
                item.PART_COUNT = partCount++;//Her paket farklı parça sayısında olması gerektiği için parça sayısı arttırılıyor.
                _context.Boxs.Update(item);
            }
            try
            {
                _context.Commit();
            }
            catch (Exception)
            {
                _context.Rollback();

                throw;
            }
        }
        private List<Part> SetParts()
        {
            _context.BeginTransaction();
            var collection = _context.Boxs.ToList();
            foreach (var item in collection)
            {
                if (_context.Parts.Any(m=>m.BOX_ID==item.BOX_ID)) continue;//Paketin parça sayıları hesaplandıysa mükerrer kayıt olmaması için o paket geçiliyor.

                var partWeight = item.WEIGHT / item.PART_COUNT;//Parça ağırlığı hesaplanıyor.
                var mod = item.WEIGHT % item.PART_COUNT;//Arta kalan ağırlık kalıp kalmadığı kontrol ediliyor.
                for (int i = 1; i <= item.PART_COUNT; i++)
                {
                    var partialBox = new Part()
                    {
                        BOX_ID = item.BOX_ID,
                        PART_WEIGHT = partWeight,
                        PART_COST = CalculatePartCost(partWeight),
                        PART_NUMBER = i,
                    };
                    if (mod != 0)//Arta kalan ağırlıklar sırayla parçalara dağıtılıyor. Mod alındığı için parça sayısından daha fazla ağırlık kalmış olamaz.
                    {
                        mod--;
                        partialBox.PART_WEIGHT++;
                        partialBox.PART_COST = CalculatePartCost(partWeight + 1);

                    }
                    _context.Parts.Add(partialBox);
                }
            }
            try
            {
                _context.Commit();
            }
            catch (Exception)
            {
                _context.Rollback();

                throw;
            }
            return _context.Parts.ToList();

        }

     
    }
}

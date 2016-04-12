using SignalrWithSqlDependency.Models;
using SignalrWithSqlDependency.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalrWithSqlDependency.Controllers
{
    public class DevTestController : Controller
    {
        DevTestSQLNotifier objRepo = new DevTestSQLNotifier();
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Test
        public ActionResult DevTestList()
        {
            return View();
        }


        public ActionResult DevTestCRUD()
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            //var getDevTestById = objRepo.GetDevTestById(Id);
            var devTest = unitOfWork.DevTestRepository.GetByID(Id);
            var devTestModel = new DevTestModel()
            {
                ID = devTest.ID,
                Date = devTest.Date,
                CampaignName = devTest.CampaignName,
                Conversions = devTest.Conversions,
                Clicks = devTest.Clicks,
                Impressions = devTest.Impressions,
                AffiliateName = devTest.AffiliateName
            };
            return View(devTestModel);
        }
        public ActionResult GetDataList()
        {
            var result = objRepo.GetData();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(DevTestModel model)
        {
           
            //  objRepo.Save(model);
            if (model.ID == 0)
            {
                DevTest dt = new DevTest()
                {
                    Date = model.Date,
                    CampaignName = model.CampaignName,
                    Conversions = model.Conversions,
                    Clicks = model.Clicks,
                    Impressions = model.Impressions,
                    AffiliateName = model.AffiliateName
                };
                unitOfWork.DevTestRepository.Insert(dt);
                unitOfWork.Save();
            }
            else
            {
                var result = unitOfWork.DevTestRepository.GetByID(model.ID);
                if (result != null)
                {
                    result.Date = model.Date;
                    result.CampaignName = model.CampaignName;
                    result.Conversions = model.Conversions;
                    result.Clicks = model.Clicks;
                    result.Impressions = model.Impressions;
                    result.AffiliateName = model.AffiliateName;
                    unitOfWork.DevTestRepository.Update(result);
                    unitOfWork.Save();
                }

            }
            return RedirectToAction("DevTestCRUD");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            //var getDevTestById = objRepo.GetDevTestById(Id);
            var devTest = unitOfWork.DevTestRepository.GetByID(Id);
            var devTestModel = new DevTestModel()
            {
                ID = devTest.ID,
                Date = devTest.Date,
                CampaignName = devTest.CampaignName,
                Conversions = devTest.Conversions,
                Clicks = devTest.Clicks,
                Impressions = devTest.Impressions,
                AffiliateName = devTest.AffiliateName
            };
            return View(devTestModel);
        }

        [HttpPost]
        public ActionResult Delete(DevTestModel model)
        {
            unitOfWork.DevTestRepository.Delete(model.ID);
            unitOfWork.Save();     
            return RedirectToAction("DevTestCRUD");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
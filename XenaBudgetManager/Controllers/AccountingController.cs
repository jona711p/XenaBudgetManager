using System;
using System.Collections.Generic;
using System.Web.Mvc;
using XenaBudgetManager.Models;

namespace XenaBudgetManager.Controllers
{
    public class AccountingController : Controller
    {
        public ActionResult Accounting()
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;

            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return View();
        }

        [HttpPost]
        public ActionResult Accounting(DateTime fromDate, DateTime toDate)
        {
            List<LedgerGroupData> ledgerGroupDataList = GetXenaData.LedgerGroupData(Session["access_token"].ToString(), fromDate, toDate);
            List<LedgerGroupDetailData> ledgerGroupDetailDataList = GetXenaData.LedgerGroupDetailData(Session["access_token"].ToString(), fromDate, toDate);

            return View();
        }

        public ActionResult LedgerTag()
        {
            GetXenaData.LedgerTag(Session["access_token"].ToString());

            return RedirectToAction("Accounting");
        }

        public ActionResult LedgerGroupData()
        {
            return View(GetXenaData.LedgerGroupData(Session["access_token"].ToString()));
        }

        public ActionResult LedgerGroupDetailData()
        {
            return View(GetXenaData.LedgerGroupDetailData(Session["access_token"].ToString()));
        }
    }

    //{
    //    // January
    //    public static int RevenueDogFoodRevenueJanuary = 10;
    //    public static int RevenueCatFoodRevenueJanuary = 10;
    //    public static int RevenueJanuary = RevenueCatFoodRevenueJanuary + RevenueDogFoodRevenueJanuary;
    //    public static int ProductConsumptionJanuaryDogFood = 10;
    //    public static int ProductConsumptionJanuaryCatFood = 10;
    //    public static int ProductConsumptionJanuary = ProductConsumptionJanuaryCatFood + ProductConsumptionJanuaryDogFood;
    //    public static int StaffCostPayRevenueJanuary = 10;
    //    public static int StaffCostRevenueJanuary = StaffCostPayRevenueJanuary;
    //    public static int LocalCostRentRevenueJanuary = 10;
    //    public static int LocalLightHeatRevenueJanuary = 10;
    //    public static int LocalCostRevenueJanuary = LocalCostRentRevenueJanuary + LocalLightHeatRevenueJanuary;
    //    public static int MonthResultJanuary = RevenueJanuary - ProductConsumptionJanuary - StaffCostRevenueJanuary - LocalCostRevenueJanuary;

    //    public ActionResult Accounting()
    //    {
    //        // Januar
    //        ObservableCollection<AccountingModel> Account = new ObservableCollection<AccountingModel>();
    //        Account.Add(new AccountingModel
    //        {
    //            Month = "Januar",
    //            Revenue = RevenueJanuary,
    //            Revenue_DogFood = RevenueDogFoodRevenueJanuary,
    //            Revenue_CatFood = RevenueCatFoodRevenueJanuary,
    //            ProductConsumption = ProductConsumptionJanuary,
    //            ProductConsumption_DogFood = ProductConsumptionJanuaryDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionJanuaryCatFood,
    //            StaffCost = StaffCostRevenueJanuary,
    //            StaffCost_Pay = StaffCostPayRevenueJanuary,
    //            LocalCost = LocalCostRevenueJanuary,
    //            LocalCost_Rent = LocalCostRentRevenueJanuary,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueJanuary,
    //            MonthResult = MonthResultJanuary
    //        });
    //        return View(Account);
    //    }
    //}
}
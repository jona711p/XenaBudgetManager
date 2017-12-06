using System;
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
            GetXenaData.LedgerGroupData(Session["access_token"].ToString(), fromDate, toDate);

            return View();
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

    //    // February
    //    public static int RevenueDogFoodRevenueFebruary = 50;
    //    public static int RevenueCatFoodRevenueFebruary = 50;
    //    public static int RevenueFebruary = RevenueDogFoodRevenueFebruary + RevenueCatFoodRevenueFebruary;
    //    public static int ProductConsumptionFebruaryDogFood = 50;
    //    public static int ProductConsumptionFebruaryCatFood = 50;
    //    public static int ProductConsumptionFebruary = ProductConsumptionFebruaryCatFood + ProductConsumptionFebruaryDogFood;
    //    public static int StaffCostPayRevenueFebruary = 50;
    //    public static int StaffCostRevenueFebruary = StaffCostPayRevenueFebruary;
    //    public static int LocalCostRentRevenueFebruary = 50;
    //    public static int LocalLightHeatRevenueFebruary = 50;
    //    public static int LocalCostRevenueFebruary = LocalCostRentRevenueFebruary + LocalLightHeatRevenueFebruary;
    //    public static int MonthResultFebruary = RevenueFebruary - ProductConsumptionFebruary - StaffCostRevenueFebruary - LocalCostRevenueFebruary;

    //    // March
    //    public static int RevenueDogFoodRevenueMarch = 75;
    //    public static int RevenueCatFoodRevenueMarch = 75;
    //    public static int RevenueMarch = RevenueDogFoodRevenueMarch + RevenueCatFoodRevenueMarch;
    //    public static int ProductConsumptionMarchDogFood = 75;
    //    public static int ProductConsumptionMarchCatFood = 75;
    //    public static int ProductConsumptionMarch = ProductConsumptionMarchCatFood + ProductConsumptionMarchDogFood;
    //    public static int StaffCostPayRevenueMarch = 75;
    //    public static int StaffCostRevenueMarch = StaffCostPayRevenueMarch;
    //    public static int LocalCostRentRevenueMarch = 75;
    //    public static int LocalLightHeatRevenueMarch = 75;
    //    public static int LocalCostRevenueMarch = LocalCostRentRevenueMarch + LocalLightHeatRevenueMarch;
    //    public static int MonthResultMarch = RevenueMarch - ProductConsumptionMarch - StaffCostRevenueMarch - LocalCostRevenueMarch;

    //    // April
    //    public static int RevenueDogFoodRevenueApril = 200;
    //    public static int RevenueCatFoodRevenueApril = 200;
    //    public static int RevenueApril = RevenueDogFoodRevenueApril + RevenueCatFoodRevenueApril;
    //    public static int ProductConsumptionAprilDogFood = 200;
    //    public static int ProductConsumptionAprilCatFood = 200;
    //    public static int ProductConsumptionApril = ProductConsumptionAprilCatFood + ProductConsumptionAprilDogFood;
    //    public static int StaffCostPayRevenueApril = 200;
    //    public static int StaffCostRevenueApril = StaffCostPayRevenueApril;
    //    public static int LocalCostRentRevenueApril = 200;
    //    public static int LocalLightHeatRevenueApril = 200;
    //    public static int LocalCostRevenueApril = LocalCostRentRevenueApril + LocalLightHeatRevenueApril;
    //    public static int MonthResultApril = RevenueApril - ProductConsumptionApril - StaffCostRevenueApril - LocalCostRevenueApril;

    //    // May
    //    public static int RevenueDogFoodRevenueMay = 500;
    //    public static int RevenueCatFoodRevenueMay = 500;
    //    public static int RevenueMay = RevenueDogFoodRevenueMay + RevenueCatFoodRevenueMay;
    //    public static int ProductConsumptionMayDogFood = 500;
    //    public static int ProductConsumptionMayCatFood = 500;
    //    public static int ProductConsumptionMay = ProductConsumptionMayDogFood + ProductConsumptionMayCatFood;
    //    public static int StaffCostPayRevenueMay = 500;
    //    public static int StaffCostRevenueMay = StaffCostPayRevenueMay;
    //    public static int LocalCostRentRevenueMay = 500;
    //    public static int LocalLightHeatRevenueMay = 500;
    //    public static int LocalCostRevenueMay = LocalCostRentRevenueMay + LocalLightHeatRevenueMay;
    //    public static int MonthResultMay = RevenueMay - ProductConsumptionMay - StaffCostRevenueMay - LocalCostRevenueMay;

    //    // June
    //    public static int RevenueDogFoodRevenueJune = 1500;
    //    public static int RevenueCatFoodRevenueJune = 1500;
    //    public static int RevenueJune = RevenueDogFoodRevenueJune + RevenueCatFoodRevenueJune;
    //    public static int ProductConsumptionJuneDogFood = 1500;
    //    public static int ProductConsumptionJuneCatFood = 1500;
    //    public static int ProductConsumptionJune = ProductConsumptionJuneDogFood + ProductConsumptionJuneCatFood;
    //    public static int StaffCostPayRevenueJune = 1500;
    //    public static int StaffCostRevenueJune = StaffCostPayRevenueJune;
    //    public static int LocalCostRentRevenueJune = 1500;
    //    public static int LocalLightHeatRevenueJune = 1500;
    //    public static int LocalCostRevenueJune = LocalCostRentRevenueJune + LocalLightHeatRevenueJune;
    //    public static int MonthResultJune = RevenueJune - ProductConsumptionJune - StaffCostRevenueJune - LocalCostRevenueJune;

    //    // July
    //    public static int RevenueDogFoodRevenueJuly = 4000;
    //    public static int RevenueCatFoodRevenueJuly = 4000;
    //    public static int RevenueJuly = RevenueDogFoodRevenueJuly + RevenueCatFoodRevenueJuly;
    //    public static int ProductConsumptionJulyDogFood = 4000;
    //    public static int ProductConsumptionJulyCatFood = 4000;
    //    public static int ProductConsumptionJuly = ProductConsumptionJulyDogFood + ProductConsumptionJulyCatFood;
    //    public static int StaffCostPayRevenueJuly = 4000;
    //    public static int StaffCostRevenueJuly = StaffCostPayRevenueJuly;
    //    public static int LocalCostRentRevenueJuly = 4000;
    //    public static int LocalLightHeatRevenueJuly = 4000;
    //    public static int LocalCostRevenueJuly = LocalCostRentRevenueJuly + LocalLightHeatRevenueJuly;
    //    public static int MonthResultJuly = RevenueJuly - ProductConsumptionJuly - StaffCostRevenueJuly - LocalCostRevenueJuly;

    //    // August
    //    public static int RevenueDogFoodRevenueAugust = 9000;
    //    public static int RevenueCatFoodRevenueAugust = 9000;
    //    public static int RevenueAugust = RevenueDogFoodRevenueAugust + RevenueCatFoodRevenueAugust;
    //    public static int ProductConsumptionAugustDogFood = 9000;
    //    public static int ProductConsumptionAugustCatFood = 9000;
    //    public static int ProductConsumptionAugust = ProductConsumptionAugustDogFood + ProductConsumptionAugustCatFood;
    //    public static int StaffCostPayRevenueAugust = 9000;
    //    public static int StaffCostRevenueAugust = StaffCostPayRevenueAugust;
    //    public static int LocalCostRentRevenueAugust = 9000;
    //    public static int LocalLightHeatRevenueAugust = 9000;
    //    public static int LocalCostRevenueAugust = LocalCostRentRevenueAugust + LocalLightHeatRevenueAugust;
    //    public static int MonthResultAugust = RevenueAugust - ProductConsumptionAugust - StaffCostRevenueAugust - LocalCostRevenueAugust;

    //    // September
    //    public static int RevenueDogFoodRevenueSeptember = 20000;
    //    public static int RevenueCatFoodRevenueSeptember = 20000;
    //    public static int RevenueSeptember = RevenueDogFoodRevenueSeptember + RevenueCatFoodRevenueSeptember;
    //    public static int ProductConsumptionSeptemberDogFood = 20000;
    //    public static int ProductConsumptionSeptemberCatFood = 20000;
    //    public static int ProductConsumptionSeptember = ProductConsumptionSeptemberDogFood + ProductConsumptionSeptemberCatFood;
    //    public static int StaffCostPayRevenueSeptember = 20000;
    //    public static int StaffCostRevenueSeptember = StaffCostPayRevenueSeptember;
    //    public static int LocalCostRentRevenueSeptember = 20000;
    //    public static int LocalLightHeatRevenueSeptember = 20000;
    //    public static int LocalCostRevenueSeptember = LocalCostRentRevenueSeptember + LocalLightHeatRevenueSeptember;
    //    public static int MonthResultSeptember = RevenueSeptember - ProductConsumptionSeptember - StaffCostRevenueSeptember - LocalCostRevenueSeptember;

    //    // Oktober
    //    public static int RevenueDogFoodRevenueOctober = 50000;
    //    public static int RevenueCatFoodRevenueOctober = 50000;
    //    public static int RevenueOctober = RevenueDogFoodRevenueOctober + RevenueCatFoodRevenueOctober;
    //    public static int ProductConsumptionOctoberDogFood = 50000;
    //    public static int ProductConsumptionOctoberCatFood = 50000;
    //    public static int ProductConsumptionOctober = ProductConsumptionOctoberDogFood + ProductConsumptionOctoberCatFood;
    //    public static int StaffCostPayRevenueOctober = 50000;
    //    public static int StaffCostRevenueOctober = StaffCostPayRevenueOctober;
    //    public static int LocalCostRentRevenueOctober = 50000;
    //    public static int LocalLightHeatRevenueOctober = 50000;
    //    public static int LocalCostRevenueOctober = LocalCostRentRevenueOctober + LocalLightHeatRevenueOctober;
    //    public static int MonthResultOctober = RevenueOctober - ProductConsumptionOctober - StaffCostRevenueOctober - LocalCostRevenueOctober;

    //    // November
    //    public static int RevenueDogFoodRevenueNovember = 125000;
    //    public static int RevenueCatFoodRevenueNovember = 125000;
    //    public static int RevenueNovember = RevenueDogFoodRevenueNovember + RevenueCatFoodRevenueNovember;
    //    public static int ProductConsumptionNovemberDogFood = 125000;
    //    public static int ProductConsumptionNovemberCatFood = 125000;
    //    public static int ProductConsumptionNovember = ProductConsumptionNovemberDogFood + ProductConsumptionNovemberCatFood;
    //    public static int StaffCostPayRevenueNovember = 125000;
    //    public static int StaffCostRevenueNovember = StaffCostPayRevenueNovember;
    //    public static int LocalCostRentRevenueNovember = 125000;
    //    public static int LocalLightHeatRevenueNovember = 125000;
    //    public static int LocalCostRevenueNovember = LocalCostRentRevenueNovember + LocalLightHeatRevenueNovember;
    //    public static int MonthResultNovember = RevenueNovember - ProductConsumptionNovember - StaffCostRevenueNovember - LocalCostRevenueNovember;

    //    // December
    //    public static int RevenueDogFoodRevenueDecember = 300000;
    //    public static int RevenueCatFoodRevenueDecember = 300000;
    //    public static int RevenueDecember = RevenueDogFoodRevenueDecember + RevenueCatFoodRevenueDecember;
    //    public static int ProductConsumptionDecemberDogFood = 300000;
    //    public static int ProductConsumptionDecemberCatFood = 300000;
    //    public static int ProductConsumptionDecember = ProductConsumptionDecemberDogFood + ProductConsumptionDecemberCatFood;
    //    public static int StaffCostPayRevenueDecember = 300000;
    //    public static int StaffCostRevenueDecember = StaffCostPayRevenueDecember;
    //    public static int LocalCostRentRevenueDecember = 300000;
    //    public static int LocalLightHeatRevenueDecember = 300000;
    //    public static int LocalCostRevenueDecember = LocalCostRentRevenueDecember + LocalLightHeatRevenueDecember;
    //    public static int MonthResultDecember = RevenueDecember - ProductConsumptionDecember - StaffCostRevenueDecember - LocalCostRevenueDecember;

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
    //        Account.Add(new AccountingModel
    //        {
    //            Month = "Februar",
    //            Revenue = RevenueFebruary,
    //            Revenue_DogFood = RevenueDogFoodRevenueFebruary,
    //            Revenue_CatFood = RevenueCatFoodRevenueFebruary,
    //            ProductConsumption = ProductConsumptionFebruary,
    //            ProductConsumption_DogFood = ProductConsumptionFebruaryDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionFebruaryCatFood,
    //            StaffCost = StaffCostRevenueFebruary,
    //            StaffCost_Pay = StaffCostPayRevenueFebruary,
    //            LocalCost = LocalCostRevenueFebruary,
    //            LocalCost_Rent = LocalCostRentRevenueFebruary,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueFebruary,
    //            MonthResult = MonthResultFebruary
    //        });
    //        Account.Add(new AccountingModel
    //        {
    //            Month = "Marts",
    //            Revenue = RevenueMarch,
    //            Revenue_DogFood = RevenueDogFoodRevenueMarch,
    //            Revenue_CatFood = RevenueCatFoodRevenueMarch,
    //            ProductConsumption = ProductConsumptionMarch,
    //            ProductConsumption_DogFood = ProductConsumptionMarchDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionMarchCatFood,
    //            StaffCost = StaffCostRevenueMarch,
    //            StaffCost_Pay = StaffCostPayRevenueMarch,
    //            LocalCost = LocalCostRevenueMarch,
    //            LocalCost_Rent = LocalCostRentRevenueMarch,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueMarch,
    //            MonthResult = MonthResultMarch

    //        });

    //        Account.Add(new AccountingModel
    //        {
    //            Month = "April",
    //            Revenue = RevenueApril,
    //            Revenue_DogFood = RevenueDogFoodRevenueApril,
    //            Revenue_CatFood = RevenueCatFoodRevenueApril,
    //            ProductConsumption = ProductConsumptionApril,
    //            ProductConsumption_DogFood = ProductConsumptionAprilDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionAprilCatFood,
    //            StaffCost = StaffCostRevenueApril,
    //            StaffCost_Pay = StaffCostPayRevenueApril,
    //            LocalCost = LocalCostRevenueApril,
    //            LocalCost_Rent = LocalCostRentRevenueApril,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueApril,
    //            MonthResult = MonthResultApril

    //        });

    //        Account.Add(new AccountingModel
    //        {
    //            Month = "May",
    //            Revenue = RevenueMay,
    //            Revenue_DogFood = RevenueDogFoodRevenueMay,
    //            Revenue_CatFood = RevenueCatFoodRevenueMay,
    //            ProductConsumption = ProductConsumptionMay,
    //            ProductConsumption_DogFood = ProductConsumptionMayDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionMayCatFood,
    //            StaffCost = StaffCostRevenueMay,
    //            StaffCost_Pay = StaffCostPayRevenueMay,
    //            LocalCost = LocalCostRevenueMay,
    //            LocalCost_Rent = LocalCostRentRevenueMay,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueMay,
    //            MonthResult = MonthResultMay

    //        });

    //        Account.Add(new AccountingModel
    //        {
    //            Month = "June",
    //            Revenue = RevenueJune,
    //            Revenue_DogFood = RevenueDogFoodRevenueJune,
    //            Revenue_CatFood = RevenueCatFoodRevenueJune,
    //            ProductConsumption = ProductConsumptionJune,
    //            ProductConsumption_DogFood = ProductConsumptionJuneDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionJuneCatFood,
    //            StaffCost = StaffCostRevenueJune,
    //            StaffCost_Pay = StaffCostPayRevenueJune,
    //            LocalCost = LocalCostRevenueJune,
    //            LocalCost_Rent = LocalCostRentRevenueJune,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueJune,
    //            MonthResult = MonthResultJune

    //        });

    //        Account.Add(new AccountingModel
    //        {
    //            Month = "July",
    //            Revenue = RevenueJuly,
    //            Revenue_DogFood = RevenueDogFoodRevenueJuly,
    //            Revenue_CatFood = RevenueCatFoodRevenueJuly,
    //            ProductConsumption = ProductConsumptionJuly,
    //            ProductConsumption_DogFood = ProductConsumptionJulyDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionJulyCatFood,
    //            StaffCost = StaffCostRevenueJuly,
    //            StaffCost_Pay = StaffCostPayRevenueJuly,
    //            LocalCost = LocalCostRevenueJuly,
    //            LocalCost_Rent = LocalCostRentRevenueJuly,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueJuly,
    //            MonthResult = MonthResultJuly

    //        });

    //        Account.Add(new AccountingModel
    //        {
    //            Month = "August",
    //            Revenue = RevenueAugust,
    //            Revenue_DogFood = RevenueDogFoodRevenueAugust,
    //            Revenue_CatFood = RevenueCatFoodRevenueAugust,
    //            ProductConsumption = ProductConsumptionAugust,
    //            ProductConsumption_DogFood = ProductConsumptionAugustDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionAugustCatFood,
    //            StaffCost = StaffCostRevenueAugust,
    //            StaffCost_Pay = StaffCostPayRevenueAugust,
    //            LocalCost = LocalCostRevenueAugust,
    //            LocalCost_Rent = LocalCostRentRevenueAugust,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueAugust,
    //            MonthResult = MonthResultAugust

    //        });

    //        Account.Add(new AccountingModel
    //        {
    //            Month = "September",
    //            Revenue = RevenueSeptember,
    //            Revenue_DogFood = RevenueDogFoodRevenueSeptember,
    //            Revenue_CatFood = RevenueCatFoodRevenueSeptember,
    //            ProductConsumption = ProductConsumptionSeptember,
    //            ProductConsumption_DogFood = ProductConsumptionSeptemberDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionSeptemberCatFood,
    //            StaffCost = StaffCostRevenueSeptember,
    //            StaffCost_Pay = StaffCostPayRevenueSeptember,
    //            LocalCost = LocalCostRevenueSeptember,
    //            LocalCost_Rent = LocalCostRentRevenueSeptember,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueSeptember,
    //            MonthResult = MonthResultSeptember

    //        });

    //        Account.Add(new AccountingModel
    //        {
    //            Month = "Oktober",
    //            Revenue = RevenueOctober,
    //            Revenue_DogFood = RevenueDogFoodRevenueOctober,
    //            Revenue_CatFood = RevenueCatFoodRevenueOctober,
    //            ProductConsumption = ProductConsumptionOctober,
    //            ProductConsumption_DogFood = ProductConsumptionOctoberDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionOctoberCatFood,
    //            StaffCost = StaffCostRevenueOctober,
    //            StaffCost_Pay = StaffCostPayRevenueOctober,
    //            LocalCost = LocalCostRevenueOctober,
    //            LocalCost_Rent = LocalCostRentRevenueOctober,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueOctober,
    //            MonthResult = MonthResultOctober

    //        });

    //        Account.Add(new AccountingModel
    //        {
    //            Month = "November",
    //            Revenue = RevenueNovember,
    //            Revenue_DogFood = RevenueDogFoodRevenueNovember,
    //            Revenue_CatFood = RevenueCatFoodRevenueNovember,
    //            ProductConsumption = ProductConsumptionNovember,
    //            ProductConsumption_DogFood = ProductConsumptionNovemberDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionNovemberCatFood,
    //            StaffCost = StaffCostRevenueNovember,
    //            StaffCost_Pay = StaffCostPayRevenueNovember,
    //            LocalCost = LocalCostRevenueNovember,
    //            LocalCost_Rent = LocalCostRentRevenueNovember,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueNovember,
    //            MonthResult = MonthResultNovember

    //        });

    //        Account.Add(new AccountingModel
    //        {
    //            Month = "December",
    //            Revenue = RevenueDecember,
    //            Revenue_DogFood = RevenueDogFoodRevenueDecember,
    //            Revenue_CatFood = RevenueCatFoodRevenueDecember,
    //            ProductConsumption = ProductConsumptionDecember,
    //            ProductConsumption_DogFood = ProductConsumptionDecemberDogFood,
    //            ProductConsumption_CatFood = ProductConsumptionDecemberCatFood,
    //            StaffCost = StaffCostRevenueDecember,
    //            StaffCost_Pay = StaffCostPayRevenueDecember,
    //            LocalCost = LocalCostRevenueDecember,
    //            LocalCost_Rent = LocalCostRentRevenueDecember,
    //            LocalCost_Light_Heat = LocalLightHeatRevenueDecember,
    //            MonthResult = MonthResultDecember

    //        });

    //        return View(Account);

    //    }

    //}
}
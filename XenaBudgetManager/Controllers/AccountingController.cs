using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XenaBudgetManager.Models;

namespace XenaBudgetManager.Controllers
{
    public class AccountingController : Controller
    {
        // January
        public static int RevenueDogFoodRevenueJanuary = 10;
        public static int RevenueCatFoodRevenueJanuary = 10;
        public static int RevenueJanuary = RevenueCatFoodRevenueJanuary+RevenueDogFoodRevenueJanuary;
        public static int ProductConsumptionJanuaryDogFood = 10;
        public static int ProductConsumptionJanuaryCatFood = 10;
        public static int ProductConsumptionJanuary = ProductConsumptionJanuaryCatFood+ProductConsumptionJanuaryDogFood;
        public static int StaffCostPayRevenueJanuary = 10;
        public static int StaffCostRevenueJanuary = StaffCostPayRevenueJanuary;
        public static int LocalCostRentRevenueJanuary = 10;
        public static int LocalLightHeatRevenueJanuary = 10;
        public static int LocalCostRevenueJanuary = LocalCostRentRevenueJanuary+LocalLightHeatRevenueJanuary;
        public static int MonthResultJanuary = RevenueJanuary- ProductConsumptionJanuary- StaffCostRevenueJanuary-LocalCostRevenueJanuary;

        // February
        public static int RevenueDogFoodRevenueFebruary = 50;
        public static int RevenueCatFoodRevenueFebruary = 50;
        public static int RevenueFebruary = RevenueDogFoodRevenueFebruary+RevenueCatFoodRevenueFebruary;
        public static int ProductConsumptionFebruaryDogFood = 50;
        public static int ProductConsumptionFebruaryCatFood = 50;
        public static int ProductConsumptionFebruary = ProductConsumptionFebruaryCatFood+ProductConsumptionFebruaryDogFood;
        public static int StaffCostPayRevenueFebruary = 50;
        public static int StaffCostRevenueFebruary = StaffCostPayRevenueFebruary;
        public static int LocalCostRentRevenueFebruary = 50;
        public static int LocalLightHeatRevenueFebruary = 50;
        public static int LocalCostRevenueFebruary = LocalCostRentRevenueFebruary+LocalLightHeatRevenueFebruary;
        public static int MonthResultFebruary = RevenueFebruary - ProductConsumptionFebruary - StaffCostRevenueFebruary - LocalCostRevenueFebruary;

        // March
        public static int RevenueDogFoodRevenueMarch = 75;
        public static int RevenueCatFoodRevenueMarch = 75;
        public static int RevenueMarch = RevenueDogFoodRevenueMarch + RevenueCatFoodRevenueMarch;
        public static int ProductConsumptionMarchDogFood = 75;
        public static int ProductConsumptionMarchCatFood = 75;
        public static int ProductConsumptionMarch = ProductConsumptionMarchCatFood + ProductConsumptionMarchDogFood;
        public static int StaffCostPayRevenueMarch = 75;
        public static int StaffCostRevenueMarch = StaffCostPayRevenueMarch;
        public static int LocalCostRentRevenueMarch = 75;
        public static int LocalLightHeatRevenueMarch = 75;
        public static int LocalCostRevenueMarch = LocalCostRentRevenueMarch + LocalLightHeatRevenueMarch;
        public static int MonthResultMarch = RevenueMarch - ProductConsumptionMarch - StaffCostRevenueMarch - LocalCostRevenueMarch;


        public ActionResult Accounting()
        {
            // Januar
            ObservableCollection<AccountingModel> Account = new ObservableCollection<AccountingModel>();
            Account.Add(new AccountingModel
            {
                Month = "Januar",
                Revenue = RevenueJanuary,
                Revenue_DogFood = RevenueDogFoodRevenueJanuary,
                Revenue_CatFood = RevenueCatFoodRevenueJanuary,
                ProductConsumption = ProductConsumptionJanuary,
                ProductConsumption_DogFood = ProductConsumptionJanuaryDogFood,
                ProductConsumption_CatFood = ProductConsumptionJanuaryCatFood,
                StaffCost = StaffCostRevenueJanuary,
                StaffCost_Pay = StaffCostPayRevenueJanuary,
                LocalCost = LocalCostRevenueJanuary,
                LocalCost_Rent = LocalCostRentRevenueJanuary,
                LocalCost_Light_Heat = LocalLightHeatRevenueJanuary,
                MonthResult = MonthResultJanuary
            });
            Account.Add(new AccountingModel
            {
                Month = "Februar",
                Revenue = RevenueFebruary,
                Revenue_DogFood = RevenueDogFoodRevenueFebruary,
                Revenue_CatFood = RevenueCatFoodRevenueFebruary,
                ProductConsumption = ProductConsumptionFebruary,
                ProductConsumption_DogFood = ProductConsumptionFebruaryDogFood,
                ProductConsumption_CatFood = ProductConsumptionFebruaryCatFood,
                StaffCost = StaffCostRevenueFebruary,
                StaffCost_Pay = StaffCostPayRevenueFebruary,
                LocalCost = LocalCostRevenueFebruary,
                LocalCost_Rent = LocalCostRentRevenueFebruary,
                LocalCost_Light_Heat = LocalLightHeatRevenueFebruary,
                MonthResult = MonthResultFebruary
            });
            Account.Add(new AccountingModel
            {
                Month = "Marts",
                Revenue = RevenueMarch,
                Revenue_DogFood = RevenueDogFoodRevenueMarch,
                Revenue_CatFood = RevenueCatFoodRevenueMarch,
                ProductConsumption = ProductConsumptionMarch,
                ProductConsumption_DogFood = ProductConsumptionMarchDogFood,
                ProductConsumption_CatFood = ProductConsumptionMarchCatFood,
                StaffCost = StaffCostRevenueMarch,
                StaffCost_Pay = StaffCostPayRevenueMarch,
                LocalCost = LocalCostRevenueMarch,
                LocalCost_Rent = LocalCostRentRevenueMarch,
                LocalCost_Light_Heat = LocalLightHeatRevenueMarch,
                MonthResult = MonthResultMarch

            });

            return View(Account);

        }

    }
}
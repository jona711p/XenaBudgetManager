﻿using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using XenaBudgetManager.Classes;

namespace XenaBudgetManager.Models
{

    public class BudgetController : Controller
    {
        ///<summary>
        /// Written by Mikael
        ///</summary>
        public ActionResult CreateBudget()
        {
            return View();
        }

        ///<summary>
        /// Written by Mikael and Thomas
        ///</summary>
        [HttpPost]
        public ActionResult CreateBudget(Budget budget)
        {
            //evt lav et tjek om budgetyear allerede er i db?

            //bind det valgte fiscalID til budget.fiscalId 
            budget.XenaFiscalID = int.Parse(Session["fiscalID"].ToString());

            //hent alle grupper i en liste
            List<LedgerAccounts> tempLedgerAccount = GetXenaData.LedgerAccount(Session["access_token"].ToString(), int.Parse(Session["fiscalID"].ToString())); //trækker grupper ud fra xena og gemmer dem i en liste af typen LedgerAccounts

            //hent alle konti fra xena
            List<LedgerTags> tempLedgerTag = GetXenaData.LedgerTag(Session["access_token"].ToString(), int.Parse(Session["fiscalID"].ToString())); //trækker kontoer ud fra xena og gemmer dem i en liste af typen LedgerTags
            List <ExtraLedgerTag> tempExtraProductTag = GetXenaData.GetProductTag(Session["access_token"].ToString());
            List<ExtraLedgerTag> tempExtraRevenueTag = GetXenaData.GetRevenueTag(Session["access_token"].ToString()); //laver listen forfra

            //konverter og tilføj konti for varegrupper og nettoomsætning og tilføj dem til kontolisten
            for (int i = 0; i < tempExtraProductTag.Count; i++)
            {
                tempLedgerTag.Add(new LedgerTags(tempExtraProductTag[i]));
            }
            for (int i = 0; i < tempExtraRevenueTag.Count; i++)
            {
                tempLedgerTag.Add(new LedgerTags(tempExtraRevenueTag[i]));
            }

            budget.budgetID = DB.WriteNewBudget(budget); //opretter nyt budget og returnere Id fra DB

            tempLedgerAccount = DB.GetAccountIDs(tempLedgerAccount);
            tempLedgerTag = DB.GetTagIDs(tempLedgerTag);
            DB.WriteNewRel_AccountPlan(tempLedgerTag, tempLedgerAccount, budget.budgetID);

            #region Konverter til budget

            

            //gemmer rel_accountplan for det givene budgetID i et keyvaluepair
            List<KeyValuePair<int, int>> AccountPlan = DB.ReadRel_AccountPlan(budget);

            //konvertere tags til accounts
            List<Account> tempAccountList = Account.ConvertLedgerTagsToAccountList(tempLedgerTag);

            //konvertere ledgerAccounts til accountgroups
            List<AccountGroup> tempGroupList = AccountGroup.ConvertLedgerAccountToAccountGroupList(tempLedgerAccount, tempAccountList, AccountPlan);


            budget.groupList = new AccountGroupListViewModel() {groupList = tempGroupList};

            //tjek hvilke group.id der hører sammen med account.id via accountplan
            for (int i = 0; i < tempGroupList.Count; i++)
            {
                tempGroupList[i].accountList = new AccountListViewModel()
                {
                    accountList = new List<Account>()
                };
                for (int j = 0; j < tempAccountList.Count; j++)
                {
                    for (int k = 0; k < AccountPlan.Count; k++)
                    {
                        if (tempGroupList[i].accountGroupID == AccountPlan[k].Key && tempAccountList[j].accountID == AccountPlan[k].Value)
                        {
                            tempGroupList[i].accountList.accountList.Add(tempAccountList[j]);
                        }
                    }
                }
            }
            for (int i = 0; i < budget.groupList.groupList.Count; i++)
            {
                if (budget.groupList.groupList[i].accountList.accountList.Count == 0)
                {
                    budget.groupList.groupList.RemoveAt(i);
                    --i;
                }
            }
            #endregion

            ViewBag.list = budget;
            budget.NewBudget = true;
            return View("EditBudget", ViewBag.list);
        }

        ///<summary>
        /// Written by Mikael and Thomas
        ///</summary>
        [HttpPost]
        public ActionResult EditBudget(Budget compeleteBudget)
        {
            if (compeleteBudget.NewBudget)
            {
                DB.WriteBudgetValues(compeleteBudget);
            }

            else
            {
                foreach (var group in compeleteBudget.groupList.groupList)
                {
                    foreach (var account in group.accountList.accountList)
                    {
                        DB.UpdateValueInterval(account);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SelectBudget(int? budgetID = null)
        {
            //vælg budget fra dropdownliste 
            SelectList budgetList = new SelectList(DB.GetBudgetId(), "budgetID", "budgetName");
            ViewBag.budgetList = budgetList;
            Budget tempBudget = new Budget();

            if (budgetID != null)
            {
                tempBudget = new Budget();
                tempBudget = DB.GetBudget((int)budgetID);
            }

            tempBudget.NewBudget = false;

            return View("EditBudget", tempBudget);
        }
    }
}


﻿using GreenPantryFrontend.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminDashboard
{
    public partial class dashboard : System.Web.UI.Page
    {
        GP_ServiceClient SR = new GP_ServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            //int trafficNum = SR.getVisitors();
            //String display = "";
            //display = "<h5 class='card-title text-uppercase text-muted mb-0'>Total traffic</h5>";
            //display = "<span class='h2 font-weight-bold mb-0'>" + trafficNum + "</span>";
            //traffic.innerHtml = dispaly

            //decimal trafficPercent = SR.getTrafficChange();
            //if(trafficPercent > 0)
            //  trafficChange.InnerHtml = "<i class='fa fa-arrow-up'></i> 3.48%

            string display = " ";

            var categories = SR.getAllCategories();
            List<double> profits = new List<double>();

            //foreach(var c in categories)
            //{
            //    var profit = SR.profitPerCat(c.ID);
            //    profits.Add(profit);
            //}" + categories[3].Name + "

            display += "<ul class='nav nav-pills justify-content-end'>";
            display += "<li class='nav-item mr-2 mr-md-0' data-toggle='chart' data-target='#chart-sales-dark'";
            display += "data-update='{\"data\":{\"labels\":[\"" + categories[0].Name + "\",\"" + categories[1].Name + "\",\"" + categories[2].Name + "\",\"" + categories[3].Name + "\",\"" + categories[4].Name + "\",\"" + categories[5].Name + "\",\"" + categories[6].Name + "\",\"" + categories[7].Name + "\"],\"datasets\":[{\"data\":[0,20,10,30,15,40,20,60,60]}]}}' data-prefix='R' data-suffix='k'>";
            display += "<a href='#' class='nav-link py-2 px-3 active' data-toggle='tab'>";
            display += "<span class='d-none d-md-block'>Month</span><span class='d-md-none'>M</span></a></li>";
            display += "<li class='nav-item' data-toggle='chart' data-target='#chart-sales-dark' data-update='{\"data\":{\"labels\":[\"This\",\"Is\",\"Going\",\"be\",\"The\",\"Death\",\"Of\",\"me\"],";
            display += "\"datasets\":[{\"data\":[0, 20, 5, 25, 10, 30, 15, 40, 40]}]}}' data-prefix='R' data-suffix='k'>";
            display += "<a href='#' class='nav-link py-2 px-3' data-toggle='tab'>";
            display += "<span class='d-none d-md-block'>Week</span><span class='d-md-none'>W</span></a></li> </ul>";

            chart1Li.InnerHtml = display;

        }
    }
}
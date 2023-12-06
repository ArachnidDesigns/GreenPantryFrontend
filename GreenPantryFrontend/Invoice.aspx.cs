using GreenPantryFrontend.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GreenPantryFrontend
{
    public partial class Invoice : System.Web.UI.Page
    {
        GP_ServiceClient SR = new GP_ServiceClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            //replace nummber with int.Parse(Request.QueryString["InvoiceID"])
            var invoice = SR.getInvoice(int.Parse(Request.QueryString["InvoiceID"]));
            if (invoice == null)
            {
                Response.Redirect("orders.aspx");
            }
            else
            {
                int userID = int.Parse(Session["LoggedInUserID"].ToString());
                if (invoice.CustomerID.Equals(userID))
                {
                    string display = " ";

                    display += "<h3 class ='h1Inv'>Invoice #" + invoice.ID + "</h3>";
                    display += "<p class ='pInvoice'>Issued: " + invoice.Date.ToShortDateString();
                    display += "<br />Order " + invoice.Status + "</p>";
                    title.InnerHtml = display;

                    var user = SR.getUser(invoice.CustomerID);

                    display = "";
                    display += "<h2 class='h2Inv'>" + user.Name + "</h2>";
                    dynamic address = SR.getPrimaryAddress(invoice.CustomerID);
                    display += "<p class='pInvoice'>" + user.Email + "</br>" + address.Number + "</p>";
                    clientInfo.InnerHtml = display;

                    display = "";

                    dynamic invoiceItems = SR.getAllInvoiceLines(invoice.ID);

                    decimal subtotal = 0;

                    foreach (InvoiceLine item in invoiceItems)
                    {
                        var product = SR.getProduct(item.ProductID);

                        display += "<tr class='service'><td class='tableitem'><p class='pInvoice itemtext'>" + product.Name + "</p></td>";
                        display += "<td class='tableitem'><p class='pInvoice itemtext'>R" + Math.Round(product.Price, 2) + "</p></td>";
                        display += "<td class='tableitem'><p class='pInvoice itemtext'>" + item.Qty + "</p></td>";
                        display += "<td class='tableitem'><p class='pInvoice itemtext'>R" + Math.Round(product.Price * item.Qty, 2) + "</p></td></tr>";
                        tableRow.InnerHtml = display;

                        subtotal += product.Price * item.Qty;
                    }
                    Subtotal.InnerHtml = "<h2 class ='h2Inv'>R" + Math.Round(subtotal, 2) + "</h2>";

                    decimal vat = subtotal * (decimal)(0.15 / 1.15);

                    Vat.InnerHtml = "<h2 class ='h2Inv'>R" + Math.Round(vat, 2) + "</h2>";

                    if (invoice.Points > 0)
                    {
                        display = "";
                        display += "<tr class='tabletitle; runat='server' id='points'><td></td><td></td>";
                        display += "<td class='Rate'><h2 class='h2Inv'>Points Used</h2></td>";
                        display += "<td class='payment' runat='server' id='pointsAmount'><h2 class ='h2Inv'>R" + invoice.Points + ".00</h2></td></tr>";
                        pointsDiv.InnerHtml = display;
                    }
                    else
                    {
                        pointsDiv.InnerHtml = "";
                    }

                    dynamic setting = SR.getSetting(1);
                    int shippingCost = int.Parse(setting.Field2);
                    if (subtotal > shippingCost)
                    {
                        deliverFree.InnerHtml = "<h2 class ='h2Inv'>R0.00</h2>";
                        Total.InnerHtml = "<h2 class ='h2Inv'>R" + Math.Round((subtotal - invoice.Points), 2) + "</h2>";
                    }
                    else
                    {
                        deliverFree.InnerHtml = "<h2 class ='h2Inv'>R60.00</h2>";
                        Total.InnerHtml = "<h2 class ='h2Inv'>R" + Math.Round((subtotal + 60 - invoice.Points), 2) + "</h2>";
                    }
                }
                else
                {
                    Response.Redirect("home.aspx");
                }
            }
        }
    }

    //this is so dumb
}
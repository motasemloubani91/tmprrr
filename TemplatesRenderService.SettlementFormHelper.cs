using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using TemplatesRender.BL.Constants;
using TemplatesRender.Dto;
using TemplatesRender.Models;
using static TemplatesRender.BL.Constants.FormsVariables;

namespace TemplatesRender.BL.Services
{
    public partial class TemplatesRenderService
    {
        private TemplateResultDto GetSettelmentFormTemp1(string id, Bitmap qrCode)
        {

            var data = _dataAccess.GetSettlmentFormData(id);
            var pageSize = 5;
            var numberOfSecondPages = 1;
            var staticData = new Dictionary<string, string>();
            var items = new Dictionary<string, string>();
            List<string> pages = new List<string>();

            staticData = CreateSettlementFormDataDictionary(data, qrCode, GetBarCode(data.BarCodeValue));


            var firstPageItemsCount = 5;
            var secondPageItemsCount = 20;
            var lastPageItemsCount = 12;

            var firstPageItems = data.TabelItems.Take(firstPageItemsCount).ToList();
            var remainingItems = data.TabelItems.Skip(firstPageItemsCount).ToList();
            var secondPagesItems = remainingItems;
            var lastPageItems = remainingItems;

            if (remainingItems.Count % secondPageItemsCount == 0)
            {
                numberOfSecondPages = (remainingItems.Count / secondPageItemsCount);
                lastPageItems = new List<SettlementItem>();
            }
            else
            {
                lastPageItemsCount = remainingItems.Count % secondPageItemsCount;
                numberOfSecondPages = (remainingItems.Count / secondPageItemsCount);
                secondPagesItems = remainingItems.Take(remainingItems.Count - lastPageItemsCount).ToList();
                lastPageItems = remainingItems.Skip(secondPagesItems.Count).ToList();
            }

            StringBuilder itemsData = new StringBuilder();
            string page;
            int pageNumber = 1;

            //prepare First Page
            for (int i = 0; i <= firstPageItems.Count - 1; i++)
            {
                itemsData.Append($@"<tr>
                    <td style=""height: 29px;"">
                        {firstPageItems[i].ForeignAmount}
                    </td>
                    <td>
                        {firstPageItems[i].LocalAmount}
                    </td>
                    <td>
                        {firstPageItems[i].GoodsDesciption}
                    </td>
                    <td>
                        {firstPageItems[i].PackagesNo}
                    </td>
                    <td>
                        {firstPageItems[i].PackagesType}
                    </td>
                </tr>");
            }
            if (firstPageItems.Count < firstPageItemsCount)
            {
                var totalRow = @"<tr>
                                <td colspan=""3"" class=""grayBg bold"">المجموع</td>
                                <td>
                                   " + staticData.First(a =>
                                         a.Key == FormsVariables.SettlementForm.TotalForeignAmount).Value + @"
                                </td>
                                <td>
                                   " + staticData.First(a =>
                                         a.Key == FormsVariables.SettlementForm.TotalLocalAmount).Value + @"
                                </td>
                            </tr>";
                staticData.Add(
                    FormsVariables.SettlementForm.TotalRow,
                    totalRow);
                staticData.Add(
                    FormsVariables.SettlementForm.TotalNumberOfPages,
                    (1).ToString());
            }
            else
            {
                staticData.Add(
                    FormsVariables.SettlementForm.TotalRow,
                    string.Empty);
                staticData.Add(
                    FormsVariables.SettlementForm.TotalNumberOfPages,
                    (numberOfSecondPages + 2).ToString());
            }
            staticData.Add(
                FormsVariables.SettlementForm.PageNumber,
                pageNumber.ToString());
            page = PrepareSFHTMLPage("settelmentFormTemp1FirstPage", itemsData.ToString(), staticData);
            pages.Add(page);
            itemsData.Clear();

            //prepare middle Pages
            if (secondPagesItems.Count > 0)
            {
                var secondPagesItemsPerPage = secondPagesItems;
                for (int j = 0; j <= numberOfSecondPages - 1; j++)
                {
                    secondPagesItemsPerPage = secondPagesItems.Skip(j * secondPageItemsCount).Take(secondPageItemsCount).ToList();
                    for (int i = 0; i <= secondPagesItemsPerPage.Count - 1; i++)
                    {
                        itemsData.Append($@"<tr>
                                <td style=""height: 29px;"">
                                    {secondPagesItemsPerPage[i].ForeignAmount}
                                </td>
                                <td>
                                    {secondPagesItemsPerPage[i].LocalAmount}
                                </td>
                                <td>
                                    {secondPagesItemsPerPage[i].GoodsDesciption}
                                </td>
                                <td>
                                    {secondPagesItemsPerPage[i].PackagesNo}
                                </td>
                                <td>
                                    {secondPagesItemsPerPage[i].PackagesType}
                                </td>
                            </tr>");
                    }
                    staticData[FormsVariables.SettlementForm.PageNumber] = (++pageNumber).ToString();
                    page = PrepareSFHTMLPage("settelmentFormTemp1MiddlePage", itemsData.ToString(), staticData);
                    pages.Add(page);
                    itemsData.Clear();
                }
            }

            //prepare last Page
            if (firstPageItems.Count >= firstPageItemsCount)
            {
                for (int i = 0; i <= lastPageItems.Count - 1; i++)
                {
                    itemsData.Append($@"<tr>
                        <td style=""height: 29px;"">
                            {lastPageItems[i].ForeignAmount}
                        </td>
                        <td>
                            {lastPageItems[i].LocalAmount}
                        </td>
                        <td>
                            {lastPageItems[i].GoodsDesciption}
                        </td>
                        <td>
                            {lastPageItems[i].PackagesNo}
                        </td>
                        <td>
                            {lastPageItems[i].PackagesType}
                        </td>
                    </tr>");
                }
                staticData[FormsVariables.SettlementForm.PageNumber] = (++pageNumber).ToString();
                page = PrepareSFHTMLPage("settelmentFormTemp1LastPage", itemsData.ToString(), staticData);
                pages.Add(page);
                itemsData.Clear();
            }

            return new TemplateResultDto()
            {
                PagesNumber = numberOfSecondPages,
                //TemplateHtml = htmlPages,
                TemplateHtmls = pages
            };
        }

        private TemplateResultDto GetSettelmentFormTemp2(string id, Bitmap qrCode)
        {

            var data = _dataAccess.GetSettlmentFormDataTemp2(id);

            var numberOfPages = 1;
            var pageSize = 5;
            var staticData = new Dictionary<string, string>();
            var items = new Dictionary<string, string>();
            List<string> pages = new List<string>();

            staticData = CreateSettlementFormDataDictionary(data, qrCode, GetBarCode(data.BarCodeValue));

            var firstPageItemsCount = 5;
            var secondPageItemsCount = 20;
            var lastPageItemsCount = 12;
            var numberOfSecondPages = 0;

            var firstPageItems = data.TabelItems.Take(firstPageItemsCount).ToList();
            var remainingItems = data.TabelItems.Skip(firstPageItemsCount).ToList();
            var secondPagesItems = remainingItems;
            var lastPageItems = remainingItems;

            if (remainingItems.Count % secondPageItemsCount == 0)
            {
                numberOfSecondPages = (remainingItems.Count / secondPageItemsCount);
                lastPageItems = new List<SettlementItem>();
            }
            else
            {
                lastPageItemsCount = remainingItems.Count % secondPageItemsCount;
                numberOfSecondPages = (remainingItems.Count / secondPageItemsCount);
                secondPagesItems = remainingItems.Take(remainingItems.Count - lastPageItemsCount).ToList();
                lastPageItems = remainingItems.Skip(secondPagesItems.Count).ToList();
            }

            StringBuilder itemsData = new StringBuilder();
            string page;
            int pageNumber = 1;

            //prepare First Page
            for (int i = 0; i <= firstPageItems.Count - 1; i++)
            {
                itemsData.Append($@"<tr>
                        <td>
                            " + firstPageItems[i].PackagesNo + @"
                        </td>
                        <td>
                            " + firstPageItems[i].PackagesType + @"
                        </td>
                        <td>
                            " + firstPageItems[i].GoodsDesciption + @"</td>
                        <td style=""padding: 0;"">
                            <div style=""height: auto;text-align: center;height: 29px; ""
                                 class=""d-flex justify-content-between "">
                                <p style=""margin: 0; width: 50%; padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                    
                            " + firstPageItems[i].ForeignAmountBefore + @"
                                </p>
                                <p style=""margin: 0; width: 50%;border-right: 1px solid #000;margin: 0;padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                    
                            " + firstPageItems[i].ForeignAmountAfter + @"
                                </p>
                            </div>
                        </td>
                        <td style=""padding: 0;"">
                            <div style=""height: auto;text-align: center; height: 29px; ""
                                 class=""d-flex justify-content-between "">
                                <p style=""margin: 0; width: 50%; padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                   
                            " + firstPageItems[i].LocalAmountBefore + @"
                                </p>
                                <p style=""margin: 0; width: 50%;border-right: 1px solid #000;margin: 0;padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                    
                            " + firstPageItems[i].LocalAmountAfter + @"
                                </p>
                            </div>
                        </td>
                    </tr>");
            }
            if (firstPageItems.Count < firstPageItemsCount)
            {
                var totalRow = @" <tr>
                <td colspan=""3"" class=""grayBg bold"">المجموع</td>
                <td style=""padding: 0;"">
                    <div style=""height: auto;text-align: center;min-height: 30px; ""
                         class=""d-flex justify-content-between"">
                        <p style=""margin: 0; width: 50%; padding: 3px;display: flex;align-items: center;justify-content: center;"">
                       " + staticData.First(a =>
                                         a.Key == FormsVariables.SettlementForm.TotalForeignAmountBefore).Value + @"
                                </p>
                        <p style=""margin: 0; width: 50%; border-right: 1px solid #000;margin: 0;padding: 3px; display: flex;align-items: center;justify-content: center;"">
                        " + staticData.First(a =>
                                         a.Key == FormsVariables.SettlementForm.TotalForeignAmountAfter).Value + @"
                                </p>
                    </div>
                </td>
                <td style=""padding: 0;"">
                    <div style=""height: auto;text-align: center;min-height: 30px; ""
                         class=""d-flex justify-content-between"">
                        <p style=""margin: 0; width: 50%; padding: 3px;display: flex;align-items: center;justify-content: center;"">
                        " + staticData.First(a =>
                                         a.Key == FormsVariables.SettlementForm.TotalLocalAmountBefore).Value + @"
                               </p>
                        <p style=""margin: 0; width: 50%;     border-right: 1px solid #000;margin: 0;padding: 3px;display: flex;align-items: center;justify-content: center;"">
                            " + staticData.First(a =>
                                         a.Key == FormsVariables.SettlementForm.TotalLocalAmountAfter).Value + @"
                        </p>
                    </div>
                </td>
            </tr>";
                staticData.Add(
                    FormsVariables.SettlementForm.TotalRow,
                    totalRow);
                staticData.Add(
                    FormsVariables.SettlementForm.TotalNumberOfPages,
                    (1).ToString());
            }
            else
            {
                staticData.Add(
                    FormsVariables.SettlementForm.TotalRow,
                    string.Empty);
                staticData.Add(
                    FormsVariables.SettlementForm.TotalNumberOfPages,
                    (numberOfSecondPages + 2).ToString());
            }
            staticData.Add(
                FormsVariables.SettlementForm.PageNumber,
                pageNumber.ToString());
            page = PrepareSFHTMLPage("settelmentFormTemp2FirstPage", itemsData.ToString(), staticData);
            pages.Add(page);
            itemsData.Clear();

            //prepare middle Pages
            if (secondPagesItems.Count > 0)
            {
                var secondPagesItemsPerPage = secondPagesItems;
                for (int j = 0; j <= numberOfSecondPages - 1; j++)
                {
                    secondPagesItemsPerPage = secondPagesItems.Skip(j * secondPageItemsCount).Take(secondPageItemsCount).ToList();
                    for (int i = 0; i <= secondPagesItemsPerPage.Count - 1; i++)
                    {
                        itemsData.Append($@"<tr>
                            <td>
                                " + secondPagesItemsPerPage[i].PackagesNo + @"
                            </td>
                            <td>
                                " + secondPagesItemsPerPage[i].PackagesType + @"
                            </td>
                            <td>
                                " + secondPagesItemsPerPage[i].GoodsDesciption + @"</td>
                            <td style=""padding: 0;"">
                                <div style=""height: auto;text-align: center;height: 29px; ""
                                     class=""d-flex justify-content-between "">
                                    <p style=""margin: 0; width: 50%; padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                    
                                " + secondPagesItemsPerPage[i].ForeignAmountBefore + @"
                                    </p>
                                    <p style=""margin: 0; width: 50%;border-right: 1px solid #000;margin: 0;padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                    
                                " + secondPagesItemsPerPage[i].ForeignAmountAfter + @"
                                    </p>
                                </div>
                            </td>
                            <td style=""padding: 0;"">
                                <div style=""height: auto;text-align: center; height: 29px; ""
                                     class=""d-flex justify-content-between "">
                                    <p style=""margin: 0; width: 50%; padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                   
                                " + secondPagesItemsPerPage[i].LocalAmountBefore + @"
                                    </p>
                                    <p style=""margin: 0; width: 50%;border-right: 1px solid #000;margin: 0;padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                    
                                " + secondPagesItemsPerPage[i].LocalAmountAfter + @"
                                    </p>
                                </div>
                            </td>
                        </tr>");
                    }
                    staticData[FormsVariables.SettlementForm.PageNumber] = (++pageNumber).ToString();
                    page = PrepareSFHTMLPage("settelmentFormTemp2MiddlePage", itemsData.ToString(), staticData);
                    pages.Add(page);
                    itemsData.Clear();
                }
            }

            //prepare last Page
            if (firstPageItems.Count >= firstPageItemsCount)
            {
                for (int i = 0; i <= lastPageItems.Count - 1; i++)
                {
                    itemsData.Append($@"<tr>
                        <td>
                            " + lastPageItems[i].PackagesNo + @"
                        </td>
                        <td>
                            " + lastPageItems[i].PackagesType + @"
                        </td>
                        <td>
                            " + lastPageItems[i].GoodsDesciption + @"</td>
                        <td style=""padding: 0;"">
                            <div style=""height: auto;text-align: center;height: 29px; ""
                                 class=""d-flex justify-content-between "">
                                <p style=""margin: 0; width: 50%; padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                    
                            " + lastPageItems[i].ForeignAmountBefore + @"
                                </p>
                                <p style=""margin: 0; width: 50%;border-right: 1px solid #000;margin: 0;padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                    
                            " + lastPageItems[i].ForeignAmountAfter + @"
                                </p>
                            </div>
                        </td>
                        <td style=""padding: 0;"">
                            <div style=""height: auto;text-align: center; height: 29px; ""
                                 class=""d-flex justify-content-between "">
                                <p style=""margin: 0; width: 50%; padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                   
                            " + lastPageItems[i].LocalAmountBefore + @"
                                </p>
                                <p style=""margin: 0; width: 50%;border-right: 1px solid #000;margin: 0;padding: 3px;"" class=""d-flex justify-content-center align-items-center"">
                                    
                            " + lastPageItems[i].LocalAmountAfter + @"
                                </p>
                            </div>
                        </td>
                    </tr>");
                }
                staticData[FormsVariables.SettlementForm.PageNumber] = (++pageNumber).ToString();
                page = PrepareSFHTMLPage("settelmentFormTemp2LastPage", itemsData.ToString(), staticData);
                pages.Add(page);
                itemsData.Clear();
            }

            return new TemplateResultDto()
            {
                PagesNumber = numberOfSecondPages,
                //TemplateHtml = htmlPages,
                TemplateHtmls = pages
            };
        }

        private Dictionary<string, string> CreateSettlementFormDataDictionary(Settlement data, Bitmap qrCode, string barCode)
        {

            var qrHtml = "";
            var barHtml = "";

            //for testing/demo purposes (QR)
            if (qrCode == null)
            {
                using (var ms = new MemoryStream(File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\agility.png"))))
                {
                    qrCode = new Bitmap(ms);
                }
            }

            if (qrCode != null)
            {
                qrHtml = "<img alt=\"\" width=\"100\" height=\"100\" src=\"data:image/png;base64," + ConvertBitmapToBase64(qrCode) + "\" /> ";

            }

            if (barCode != null)
            {
                barHtml = "data:image/png;base64," + barCode;
            }

            var items = new Dictionary<string, string>();

            items.Add(FormsVariables.SettlementForm.IsPayment, data.IsPayment);

            items.Add(FormsVariables.PromissoryNoteForNonGvtForm.QrCode, qrHtml);
            items.Add(FormsVariables.PromissoryNoteForNonGvtForm.BarCode, barHtml);

            items.Add(FormsVariables.SettlementForm.AdjustmentSupplementNo, data.AdjustmentSupplementNo);
            items.Add(FormsVariables.SettlementForm.Date, data.Date);
            items.Add(FormsVariables.SettlementForm.SettlementStatementNo, data.SettlementStatementNo);
            items.Add(FormsVariables.SettlementForm.TradeLicenseNo, data.TradeLicenseNo);
            items.Add(FormsVariables.SettlementForm.SettlementDate, data.SettlementDate);
            items.Add(FormsVariables.SettlementForm.Reason, data.Reasons);
            items.Add(FormsVariables.SettlementForm.SupplementAuthor, data.SupplementAuthor);
            items.Add(FormsVariables.SettlementForm.ConsigneeName, data.ConsigneeName);
            items.Add(FormsVariables.SettlementForm.Notes, data.Notes);
            items.Add(FormsVariables.SettlementForm.AmountDescription, data.AmountDescription);
            items.Add(FormsVariables.SettlementForm.AmountDescriptionAfter, data.AmountDescriptionAfter);
            items.Add(FormsVariables.SettlementForm.AmountDescriptionBefore, data.AmountDescriptionBefore);
            items.Add(FormsVariables.SettlementForm.TotalAmount, data.TotalAmount);
            items.Add(FormsVariables.SettlementForm.AmountInCharacter, data.AmountInCharacter);
            items.Add(FormsVariables.SettlementForm.PaymentMode, data.PaymentMethod);
            items.Add(FormsVariables.SettlementForm.ReceiptNo, data.ReceiptNo);
            items.Add(FormsVariables.SettlementForm.ReceiptDate, data.ReceiptDate);
            items.Add(FormsVariables.SettlementForm.Amount, data.Amount);
            items.Add(FormsVariables.SettlementForm.Process, data.Process);
            items.Add(FormsVariables.SettlementForm.ReceiptAmount, data.ReceiptAmount);
            items.Add(FormsVariables.SettlementForm.TotalForeignAmount, data.TotalForeignAmount);
            items.Add(FormsVariables.SettlementForm.TotalLocalAmount, data.TotalLocalAmount);
            items.Add(FormsVariables.SettlementForm.TotalForeignAmountAfter, data.TotalForeignAmountAfter);
            items.Add(FormsVariables.SettlementForm.TotalForeignAmountBefore, data.TotalForeignAmountBefore);
            items.Add(FormsVariables.SettlementForm.TotalLocalAmountAfter, data.TotalLocalAmountAfter);
            items.Add(FormsVariables.SettlementForm.TotalLocalAmountBefore, data.TotalLocalAmountBefore);
            items.Add(FormsVariables.SettlementForm.BrokerLicNo, data.BrokerLicNo);
            items.Add(FormsVariables.SettlementForm.AuditorName, data.AuditorName);
            items.Add(FormsVariables.SettlementForm.FormName, data.FormName);
            items.Add(FormsVariables.SettlementForm.LetterDetails, data.LetterDetails);


            return items;
        }

        private string PrepareSFHTMLPage(
            string pageName,
            string itemsData,
            Dictionary<string, string> staticData)
        {
            string IsPaymentDiv = @"<p>
                            طريقة الدفع:
                            <span>" + staticData.First(a => a.Key == SettlementForm.PaymentMode).Value + @"</span>
                        </p>
                        <p>
                            رقم الايصال:
                            <span>" + staticData.First(a => a.Key == SettlementForm.ReceiptNo).Value + @"</span>
                        </p>
                        <p>
                            تاريخ الايصال:
                            <span>" + staticData.First(a => a.Key == SettlementForm.ReceiptDate).Value + @"</span>
                        </p>
                        <p>
                            المبلغ:
                            <span>" + staticData.First(a => a.Key == SettlementForm.Amount).Value + @"</span>
                        </p>";
            string IsRefundDiv = @"<p>
                            العملية:
                            <span>استرداد</span>
                        </p>
                        <p>
                            مبلغ الاسترداد:
                            <span>" + staticData.First(a => a.Key == SettlementForm.ReceiptAmount).Value + @"</span>
                        </p>";

            Dictionary<string, string> dataToBeReplaced = new Dictionary<string, string>();
            dataToBeReplaced = dataToBeReplaced.Concat(staticData).ToDictionary(a => a.Key, a => a.Value); ;

            dataToBeReplaced.Add(SettlementForm.ItemsData, itemsData);

            if (staticData.Any(a => a.Key == SettlementForm.IsPayment && a.Value == "1"))
            {
                dataToBeReplaced.Add(SettlementForm.PaymentData, IsPaymentDiv);
                dataToBeReplaced.Add(SettlementForm.FormNameType, "دفع");
            }
            else
            {
                dataToBeReplaced.Add(SettlementForm.PaymentData, IsRefundDiv);
                dataToBeReplaced.Add(SettlementForm.FormNameType, "استرداد");
            }

            var page = ReplaceHtmlData(dataToBeReplaced, pageName);
            return page;
        }

        private void PrepareSFHTMLPage(
            Dictionary<string, string> items,
            Dictionary<string, string> staticData,
            List<string> pages,
            string template, bool isLastPage = false)
        {
            string IsPaymentDiv = @"            <p>
                            طريقة الدفع:
                            <span>{{Payment-Mode}}</span>
                        </p>
                        <p>
                            رقم الايصال:
                            <span>{{Receipt-No}}</span>
                        </p>
                        <p>
                            تاريخ الايصال:
                            <span>{{Receipt-date}}</span>
                        </p>
                        <p>
                            المبلغ:
                            <span>{{amount}}</span>
                        </p>";
            string IsRefundDiv = @"   <p>
                            العملية:
                            <span>استرداد</span>
                        </p>
                        <p>
                            مبلغ الاسترداد:
                            <span>{{Receipt-amount}}</span>
                        </p>";

            foreach (var item in staticData)
            {
                items.Add(item.Key, item.Value);
            }

            var res = ReplaceHtmlData(items, template);

            if (items.Any(a => a.Key == SettlementForm.IsPayment && a.Value == "1"))
            {
                res = res.Replace(SettlementForm.PaymentData, IsPaymentDiv);
                res = res.Replace(SettlementForm.PaymentMode, items.First(a => a.Key == SettlementForm.PaymentMode).Value);
                res = res.Replace(SettlementForm.ReceiptNo, items.First(a => a.Key == SettlementForm.ReceiptNo).Value);
                res = res.Replace(SettlementForm.ReceiptDate, items.First(a => a.Key == SettlementForm.ReceiptDate).Value);
                res = res.Replace(SettlementForm.Amount, items.First(a => a.Key == SettlementForm.Amount).Value);
                res = res.Replace(SettlementForm.FormNameType, "دفع");


            }
            else if (items.Any(a => a.Key == SettlementForm.IsPayment && a.Value == "0"))
            {
                res = res.Replace(SettlementForm.PaymentData, IsRefundDiv);
                res = res.Replace(SettlementForm.ReceiptAmount, items.First(a => a.Key == SettlementForm.ReceiptAmount).Value);
                res = res.Replace(SettlementForm.FormNameType, "استرداد");

            }
            else
            {
                res = res.Replace(SettlementForm.PaymentData, IsRefundDiv);
                res = res.Replace(SettlementForm.ReceiptAmount, items.First(a => a.Key == SettlementForm.ReceiptAmount).Value);
                res = res.Replace(SettlementForm.FormNameType, "استرداد");

            }
            if (isLastPage)
            {
                res = res.Replace("{{display-amount}}", "display");

            }
            else
            {
                res = res.Replace("{{display-amount}}", "none");


            }

            pages.Add(res);
        }
    }
}

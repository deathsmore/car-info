using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using DVG.AP.Cms.CarInfo.Application.Contracts.Extensions;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DVG.AP.Cms.CarInfo.Application.Features.Model.NewCarArticle.Strategies.GoogleAMP
{
    public class PhilkotseGoogleAMPStrategy : IGoogleAMPStrategy
    {
        public string ConvertContentToAMP(string? content)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                    return content;

                string contentStandardized = StandardizedContent(content, StaticVariables.AMPSettings!.StorageDomain, StaticVariables.AMPSettings!.PublishDomain);
                string googleAMPContent = AMPUtils.GenerateHtmlContent(contentStandardized);
                return googleAMPContent;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static string StandardizedContent(string content, string currentViewDomain, string publishDomain, bool nofollowExternalLink = false)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            try
            {
                content = StandardizedVideoContainer(content);
                //content = content.Replace(Webconfig.ServerImage, currentViewDomain);//T-TEMP, APM cho site philkotse thì ko cần replace domain storage

                var doc = new HtmlDocument();
                doc.LoadHtml(content);
                var listNode = doc.DocumentNode.SelectNodes("//*");
                if (listNode != null && listNode.Any())
                {
                    foreach (HtmlNode node in listNode)
                    {
                        if (node.Name == "table")
                        {
                            if (node.ParentNode.Name != "div")
                            {
                                var newChild = HtmlNode.CreateNode("<div class='divresponsive'>" + node.OuterHtml + "</div>");
                                node.ParentNode.ReplaceChild(newChild, node);
                            }
                            else if (!node.ParentNode.Attributes.Contains("class") || !node.ParentNode.Attributes["class"].Value.Contains("divresponsive"))
                            {
                                node.ParentNode.AddClass("divresponsive");
                            }
                        }
                        if (node.Name == "a")
                        {
                            if (!node.Attributes.Contains("href"))
                                continue;

                            var url = node.Attributes["href"].Value.Replace("www.", "");
                            node.SetAttributeValue("href", url);

                            if (!node.Attributes["href"].Value.ToLower().Contains(publishDomain.TrimEnd('/').ToLower()))
                            {
                                if (nofollowExternalLink)
                                {
                                    node.SetAttributeValue("rel", "nofollow");
                                }
                                else if (node.Attributes["rel"] != null)
                                {
                                    node.Attributes.Remove(node.Attributes["rel"]);
                                }
                            }
                            else if (node.Attributes["rel"] != null)
                            {
                                node.Attributes.Remove(node.Attributes["rel"]);
                            }
                        }
                    }
                }
                content = doc.DocumentNode.OuterHtml;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return content;
        }

        public static string StandardizedVideoContainer(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            try
            {
                content = HttpUtility.HtmlDecode(content);
                content = content.Replace("video-container", string.Empty);

                var doc = new HtmlDocument();
                doc.LoadHtml(content);
                var listNode = doc.DocumentNode.SelectNodes("//*");
                if (listNode != null && listNode.Any())
                {
                    foreach (HtmlNode node in listNode)
                    {
                        if (node.Name == "iframe" && node.Attributes.Contains("src") && node.Attributes["src"].Value.Contains("youtu"))
                        {
                            var parentNode = node.ParentNode;
                            if (parentNode.Name == "em")
                            {
                                parentNode.Name = "div";
                            }

                            if (parentNode.ChildNodes.Count == 1)
                            {
                                if (!parentNode.Attributes.Contains("class") || !parentNode.Attributes["class"].Value.Contains("video-container"))
                                {
                                    parentNode.AddClass("video-container");
                                }
                            }
                            else
                            {
                                HtmlNode div = doc.CreateElement("div");
                                div.AddClass("video-container");
                                div.ChildNodes.Add(node);
                                parentNode.InsertBefore(div, node);
                                node.Remove();
                            }
                        }
                    }
                }
                content = doc.DocumentNode.OuterHtml;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return content;
        }
    }
}

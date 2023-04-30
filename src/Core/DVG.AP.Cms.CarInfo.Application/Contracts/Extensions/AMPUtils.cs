using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Extensions
{
    public static class AMPUtils
    {
        public static string GenerateHtmlContent(string content)
        {
            #region Regex optimization content

            string splitReg = @"(?:\s|\t|\n|\r|&nbsp;)*<\/?(?:p\s+[^>]span|div[^>]*)>(?:\s|\t|\n|\r|&nbsp;)*";
            string removeTagReg = @"(?:\s|\t|\n|\r|&nbsp;)*<\/?(?:(?:p\s+|div|br|\??[a-z0-9\-]+\:[a-z0-9\-]+)[^>])>(?:\s|\t|\n|\r|&nbsp;)*|<\!--[\S\s]*-->";
            string styleTagReg = @"<\/?(?:(?:p\s+|em|u|b|strong|<i\/?>|font|span|\??[a-z0-9\-]+\:[a-z0-9\-]+)[^>]*)>";
            string trimReg = @"^(?:\s|\t|\n|\r|&nbsp;|<br\/?>)+|(?:\s|\t|\n|\r|&nbsp;|<br\/?>)+$";
            string photoReg = @"<img[^>]+>";
            string beginValidTag = @"^(?:\s|\t|\n|\r|&nbsp;)*<(?:div[^>]*|p\s+[^>]*|p|h\d[^>]*)>";
            //string hasTag = @"<\/?[a-z0-9]+[^>]*>";

            string stand = @"^(?:(?:\s|\t|\n|\r|&nbsp;)*<\/?(?:span|div|p|br)+[^>]*>(?:\s|\t|\n|\r|&nbsp;)*)+$";


            string[] paragraphs = Regex.Split(content, splitReg, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (paragraphs != null && paragraphs.Length > 0)
            {
                for (int i = 0; i < paragraphs.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(paragraphs[i]))
                    {
                        paragraphs[i] = string.Empty;
                        continue;
                    }

                    paragraphs[i] = Regex.Replace(paragraphs[i], trimReg, string.Empty).Trim();
                    Match match = Regex.Match(paragraphs[i], stand);
                    if (match.Success)
                    {
                        paragraphs[i] = string.Empty;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(paragraphs[i]))
                        {
                            Match matchValidTag = Regex.Match(paragraphs[i], beginValidTag);
                            Match matchPhotoTag = Regex.Match(paragraphs[i], photoReg);

                            if (!matchValidTag.Success && !matchPhotoTag.Success)
                            {
                                paragraphs[i] = Regex.Replace(paragraphs[i], removeTagReg, " ").Trim();
                                paragraphs[i] = Regex.Replace(paragraphs[i], styleTagReg, string.Empty).Trim();
                                if (!string.IsNullOrWhiteSpace(paragraphs[i]))
                                {
                                    paragraphs[i] = string.Format("<p>{0}</p>", paragraphs[i]);
                                }
                            }
                        }
                    }
                }

                content = string.Join("", paragraphs);
                content = Regex.Replace(content, trimReg, "");

            }


            //content = Regex.Replace(content, @"(style=[""'](.+?)[""'].*?)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            content = Regex.Replace(content, @"<o:p></o:p>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            content = Regex.Replace(content, @"(?<empty>\<.?>(\s+)?</.?>)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            content = Regex.Replace(content, ">(\\s+)<", ">\r\n<", RegexOptions.Singleline | RegexOptions.IgnoreCase);


            #endregion

            #region Optimize Image

            Regex regAMPImage = new Regex(@"<img(?<attr>[^>]+)\>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            content = regAMPImage.Replace(content, delegate (Match m)
            {
                string patternAttrs = @"\s([a-z\-]+)=[""'](.*?)[""']";

                Regex regexAttrs = new Regex(patternAttrs, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                var matchAttrs = regexAttrs.Matches(m.Value);

                string width = "480";
                string height = "270";
                string src = string.Empty;
                string title = string.Empty;
                string alt = string.Empty;

                foreach (Match matchAttr in matchAttrs)
                {
                    switch (matchAttr.Groups[1].Value)
                    {
                        //case "width":
                        //case "data-width":
                        //    width = string.Format(" width=\"{0}\" ", (!string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? matchAttr.Groups[2].Value : width));
                        //    break;
                        //case "height":
                        //case "data-height":
                        //    height = string.Format(" height=\"{0}\" ", (!string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? matchAttr.Groups[2].Value : height));
                        //    break;
                        // hotfix lấy width height trong style của image để build trường w h cho AMP

                        case "style":
                            {
                                var style = matchAttr.Groups[2].Value;
                                if (style != null && style.Length > 0)
                                {
                                    var regex = new Regex(@"([\w-]+)\s*:\s*([^;]+)");
                                    var matchStyle = regex.Match(style);
                                    while (matchStyle.Success)
                                    {
                                        var key = matchStyle.Groups[1].Value;
                                        var value = matchStyle.Groups[2].Value;
                                        switch (key)
                                        {
                                            case "width":
                                                width = string.Format(" width=\"{0}\" ", (!string.IsNullOrWhiteSpace(value) ? value.Replace("px", "").Trim() : width));
                                                break;
                                            case "height":
                                                height = string.Format(" height=\"{0}\" ", (!string.IsNullOrWhiteSpace(value) ? value.Replace("px", "").Trim() : height));
                                                break;
                                        }
                                        matchStyle = matchStyle.NextMatch();
                                    }
                                }
                                break;
                            }
                        case "src":
                            src = !string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? string.Format(" src=\"{0}\" ", matchAttr.Groups[2].Value) : "";
                            break;
                        case "alt":
                            alt = !string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? string.Format(" alt=\"{0}\" ", StringUtils.RemoveSpecial(matchAttr.Groups[2].Value)) : "";
                            break;
                        case "title":
                            title = !string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? string.Format(" title=\"{0}\" ", StringUtils.RemoveSpecial(matchAttr.Groups[2].Value)) : "";
                            break;
                    }
                }

                title = StringUtils.RemoveSpecial(title);

                if (!string.IsNullOrWhiteSpace(alt))
                {
                    alt = StringUtils.RemoveSpecial(alt);
                }
                else
                {
                    alt = title;
                }

                if (!string.IsNullOrWhiteSpace(src))
                {
                    if (width == "480")
                        width = string.Format(" width=\"{0}\" ", width);
                    if (height == "270")
                        height = string.Format(" height=\"{0}\" ", height);

                    string attrs = string.Concat(src, width, height, alt, title);

                    return string.Format("<amp-img layout=\"responsive\" {0} lightbox></amp-img>", attrs);
                }

                return string.Empty;
            });

            #endregion

            #region optimize table tag

            string patternTable = @"<table(?<attr>[^>]+)\>";

            Regex regexFindTable = new Regex(patternTable, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            content = regexFindTable.Replace(content, delegate (Match m)
            {
                string patternAttrs = @"\s([a-z\-]+)=[""'](.*?)[""']";

                Regex regexAttrs = new Regex(patternAttrs, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                var matchAttrs = regexAttrs.Matches(m.Value);

                string width = string.Empty;
                //string height = string.Empty;
                string cellpadding = "0";
                string cellspacing = "0";
                string border = "0";
                string align = "center";

                foreach (Match matchAttr in matchAttrs)
                {
                    switch (matchAttr.Groups[1].Value)
                    {
                        case "width":
                            width = string.Format("width=\"{0}\"", !string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? matchAttr.Groups[2].Value : string.Empty);
                            break;
                        //case "height":
                        //    height = string.Format("height=\"{0}\"", !string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? matchAttr.Groups[2].Value : string.Empty);
                        //    break;
                        case "cellpadding":
                            cellpadding = !string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? matchAttr.Groups[2].Value : "0";
                            break;
                        case "cellspacing":
                            cellspacing = !string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? matchAttr.Groups[2].Value : "0";
                            break;
                        case "border":
                            border = !string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? matchAttr.Groups[2].Value : "1";
                            break;
                        case "align":
                            align = !string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? matchAttr.Groups[2].Value : "center";
                            break;
                    }
                }

                string output = $"<table {width} cellpadding=\"{cellpadding}\" cellspacing=\"{cellspacing}\" border=\"{border}\" align=\"{align}\">";

                return output;
            });

            #endregion

            #region optimize iframe tag

            string patternFrame = @"<iframe(?<attr>[^>]+)\>.*?\<\/iframe>";

            Regex regexFindIFrame = new Regex(patternFrame, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            content = regexFindIFrame.Replace(content, delegate (Match m)
            {
                string patternAttrs = @"\s([a-z\-]+)=[""'](.*?)[""']";

                Regex regexAttrs = new Regex(patternAttrs, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                var matchAttrs = regexAttrs.Matches(m.Value);

                string src = string.Empty;
                string width = "width=\"{0}\"";
                string height = "height=\"{0}\"";


                foreach (Match matchAttr in matchAttrs)
                {
                    switch (matchAttr.Groups[1].Value)
                    {
                        case "src":
                            src = matchAttr.Groups[2].Value;
                            break;
                        case "width":
                            width = string.Format(width, !string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? matchAttr.Groups[2].Value : string.Empty);
                            break;
                        case "height":
                            height = string.Format(height, !string.IsNullOrWhiteSpace(matchAttr.Groups[2].Value) ? matchAttr.Groups[2].Value : string.Empty);
                            break;
                    }

                }

                string output = string.Empty;

                Regex regexYoutube = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)(?<ID>[a-zA-Z0-9-_]+)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                Match matchYoutube = regexYoutube.Match(src);

                if (matchYoutube.Success)
                {
                    if (string.IsNullOrWhiteSpace(width)) width = "width=\"640\"";
                    if (string.IsNullOrWhiteSpace(height)) height = "height=\"480\"";
                    output = $"<amp-youtube data-videoid=\"{matchYoutube.Groups["ID"].Value}\" layout=\"responsive\" {width} {height}></amp-youtube>";
                }
                else
                {
                    output = $"<amp-iframe src=\"{src}\" {width} {height} frameborder=\"0\" allowfullscreen=\"\" sandbox=\"allow-scripts allow-same-origin\" sizes=\"(min-width: 640px) 640px, 100vw\" class=\"amp-wp-enforced-sizes\"></amp-iframe>";
                }

                return output;
            });

            #endregion

            content = content.Replace("dataid", "data-id");
            content = content.Replace("dataordinal", "data-ordinal");
            content = content.Replace("center=\"\"", "");
            content = content.Replace("text-align:=\"\"", "");

            return content;

        } 
    }
}

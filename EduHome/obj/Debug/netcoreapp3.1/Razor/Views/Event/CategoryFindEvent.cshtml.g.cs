#pragma checksum "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f3cf18102cb3bc6074f8b10881c0642e457064e1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Event_CategoryFindEvent), @"mvc.1.0.view", @"/Views/Event/CategoryFindEvent.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\_ViewImports.cshtml"
using EduHome.Model;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\_ViewImports.cshtml"
using EduHome.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\_ViewImports.cshtml"
using EduHome.Dal;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\_ViewImports.cshtml"
using EduHome.Interfaces;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f3cf18102cb3bc6074f8b10881c0642e457064e1", @"/Views/Event/CategoryFindEvent.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8956c8ef023a467bdae6c7ad0f2986b72e7f4374", @"/Views/_ViewImports.cshtml")]
    public class Views_Event_CategoryFindEvent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Event>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("event"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "event", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "detail", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("default-btn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
   ViewData["Title"] = "CategoryFindEvent"; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"




<!-- Banner Area Start -->
<div class=""banner-area-wrapper"">
    <div class=""banner-area text-center"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-xs-12"">
                    <div class=""banner-content-wrapper"">
                        <div class=""banner-content"">
                            <h2>courses</h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Banner Area End -->
<!-- Event Start -->
<div class=""event-area three text-center pt-150 pb-150"">
    <div class=""container"">
        <div class=""row"">
");
#nullable restore
#line 29 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
             foreach (Event events in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"col-md-4 col-sm-6 col-xs-12\">\n    <div class=\"single-event mb-60\">\n        <div class=\"event-img\">\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f3cf18102cb3bc6074f8b10881c0642e457064e16535", async() => {
                WriteLiteral("\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "f3cf18102cb3bc6074f8b10881c0642e457064e16804", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 1008, "~/assets/img/event/", 1008, 19, true);
#nullable restore
#line 35 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
AddHtmlAttributeValue("", 1027, events.Image, 1027, 13, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n                <div class=\"course-hover\">\n                    <i class=\"fa fa-link\"></i>\n                </div>\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 34 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                                                            WriteLiteral(events.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n            <div class=\"event-date\">\n                <h3>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f3cf18102cb3bc6074f8b10881c0642e457064e110861", async() => {
#nullable restore
#line 41 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                                                                                       Write(events.StartTime.ToString("hh"));

#line default
#line hidden
#nullable disable
                WriteLiteral(" <span>");
#nullable restore
#line 41 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                                                                                                                              Write(events.StartTime.ToString("MMMM").Substring(0, 3));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 41 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                                                                    WriteLiteral(events.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</h3>\n            </div>\n\n        </div>\n        <div class=\"event-content text-left\">\n            <h4>\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f3cf18102cb3bc6074f8b10881c0642e457064e114206", async() => {
                WriteLiteral("\n");
#nullable restore
#line 48 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                     if (events.Name.Length > 22)
                    {
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 50 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
Write(events.Name.Substring(0, 21));

#line default
#line hidden
#nullable disable
#nullable restore
#line 50 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                                 ;
}
else
{

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
Write(events.Name);

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 47 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                                                                WriteLiteral(events.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n            </h4>\n            <ul>\n                <li><span>time:</span> ");
#nullable restore
#line 59 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                                  Write(events.StartTime.ToString("hh"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" AM - ");
#nullable restore
#line 59 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                                                                        Write(events.EndTime.ToString("hh"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" PM</li>\n                <li><span>venue</span> ");
#nullable restore
#line 60 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                                  Write(events.Venue);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n            </ul>\n            <div class=\"event-content-right\">\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f3cf18102cb3bc6074f8b10881c0642e457064e119144", async() => {
                WriteLiteral("join now");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 63 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                                                                                    WriteLiteral(events.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n            </div>\n        </div>\n    </div>\n</div>            ");
#nullable restore
#line 67 "C:\Users\ROG\Desktop\P228_ASP_08-12-2022_BackEndTehmplate-main\EduHomeBackFinal-master\EduHome\Views\Event\CategoryFindEvent.cshtml"
                  }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n        </div>\n    </div>\n</div>\n<!-- Event End -->\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Event>> Html { get; private set; }
    }
}
#pragma warning restore 1591

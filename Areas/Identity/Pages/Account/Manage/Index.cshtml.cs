// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ETMP.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        public string Gender { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 

        /*public string CompanyName { get; set; }
       
        public string CompanyMailingAddress { get; set; }*/

        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            /*[CompanyName]
            [Display(Name = "Company Name")]
            public string CompanyName { get; set; }*/

            /*[CompanyMailingAddress]
            [Display(Name = "Company Mailing Address")]
            public string CompanyMailingAddress { get; set; }*/
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var gender = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            /*var companyName = await _userManager.GetCompanyNameAsync(user);
            var companyMailingAddress = await _userManager.GetCompanyMailingAddressAsync(user);*/

            Username = userName;
            Gender = gender;
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                /*CompanyName = companyName,
                CompanyMailingAddress= companyMailingAddress*/
            };


        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            /*var companyName = await _userManager.GetCompanyNameAsync(user);
            if (Input.companyName != companyName)
            {
                var setcompanyNameResult = await _userManager.SetCompanyNameAsync(user, Input.CompanyName);
                if (!setcompanyNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set new Company Name.";
                    return RedirectToPage();
                }
            }

            var companyMailingAddress = await _userManager.GetCompanyMailingAddressAsync(user);
            if (Input.companyMailingAddress != companyMailingAddress)
            {
                var setcompanyMailingAddress = await _userManager.SetcompanyMailingAddress(user, Input.CompanyMailingAddress);
                if (!setcompanyNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set new Company Mailing Address.";
                    return RedirectToPage();
                }
            }*/

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

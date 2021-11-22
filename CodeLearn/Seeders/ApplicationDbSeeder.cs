using CodeLearn.Data;
using CodeLearn.Models;
using CodeLearn.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Seeders
{
    public class ApplicationDbSeeder : IApplicationDbSeeder
    {
        private readonly IDbContextFactory<ApplicationDBContext> _contextFactory;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbSeeder(IDbContextFactory<ApplicationDBContext> contextFactory, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _contextFactory = contextFactory;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedPostManagerData()
        {
            if (await _roleManager.RoleExistsAsync("Admin") == false)
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            if (await _roleManager.RoleExistsAsync("User") == false)
                await _roleManager.CreateAsync(new IdentityRole("User"));

            if (await _userManager.FindByEmailAsync("seed_user1@gmail.com") != null)
                return;

            var identityUser1 = new IdentityUser
            {
                Id = "3dd2fdd9-ae05-49dd-8dd2-5fa9d2e99ac4",
                UserName = "seed_user1",
                Email = "seed_user1@gmail.com",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(identityUser1, "seed_user1");

            await _userManager.AddToRoleAsync(identityUser1, "User");

            var identityUser2 = new IdentityUser
            {
                Id = "513879b5-9105-487d-946a-25e82d9c99fa",
                UserName = "seed_user2",
                Email = "seed_user2@gmail.com",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(identityUser2, "seed_user2");

            await _userManager.AddToRoleAsync(identityUser2, "User");

            var identityAdmin = new IdentityUser
            {
                Id = "168313a7-cfb8-4392-ae96-6c76c712d909",
                UserName = "seed_admin",
                Email = "seed_admin@gmail.com",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(identityAdmin, "seed_admin");

            await _userManager.AddToRoleAsync(identityAdmin, "Admin");

            using var context = _contextFactory.CreateDbContext();


            var user1 = new User
            {
                Id = Guid.Parse(identityUser1.Id),
                Name = identityUser1.UserName,
                Email = identityUser1.Email,
                DateJoined = DateTime.ParseExact("2012-07-22", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                IsBlocked = false,
                Bio = "seed user 1 bio, hello!",
            };
            context.Users.Add(user1);

            var user2 = new User
            {
                Id = Guid.Parse(identityUser2.Id),
                Name = identityUser2.UserName,
                Email = identityUser2.Email,
                DateJoined = DateTime.ParseExact("2020-11-22", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                IsBlocked = false,
                Bio = "seed user 2 bio, hi there!",
            };
            context.Users.Add(user2);

            var admin = new User
            {
                Name = identityAdmin.UserName,
                Email = identityAdmin.Email,
                DateJoined = DateTime.ParseExact("2009-09-09", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                IsBlocked = false,
                Bio = "seed admin bio, get banned!",
            };
            context.Users.Add(admin);

            var post1 = new Post
            {
                Id = Guid.Parse("72633b00-cdb2-47b7-810f-715562fcab52"),
                UserId = Guid.Parse(identityUser1.Id),
                Title = "Seed Post 1: Tiny MCE Basic Example",
                Slug = "seed-post-tiny-mce-basic-example",
                Content = "<p><img style=\"display: block; margin-left: auto; margin-right: auto;\" title=\"Tiny Logo\" src=\"https://www.tiny.cloud/docs/images/logos/android-chrome-256x256.png\" alt=\"TinyMCE Logo\" width=\"128\" height=\"128\" /></p>\n<h2 style=\"text-align: center;\">Welcome to the TinyMCE editor demo!</h2>\n<h2>Got questions or need help?</h2>\n<ul>\n<li>Our <a href=\"https://www.tiny.cloud/docs/\">documentation</a> is a great resource for learning how to configure TinyMCE.</li>\n<li>Have a specific question? Try the <a href=\"https://stackoverflow.com/questions/tagged/tinymce\" target=\"_blank\" rel=\"noopener\"><code>tinymce</code> tag at Stack Overflow</a>.</li>\n<li>We also offer enterprise grade support as part of <a href=\"https://www.tiny.cloud/pricing\">TinyMCE premium plans</a>.</li>\n</ul>\n<h2>A simple table to play with</h2>\n<table style=\"border-collapse: collapse; width: 100%;\" border=\"1\">\n<thead>\n<tr>\n<th>Product</th>\n<th>Cost</th>\n<th>Really?</th>\n</tr>\n</thead>\n<tbody>\n<tr>\n<td>TinyMCE</td>\n<td>Free</td>\n<td>YES!</td>\n</tr>\n<tr>\n<td>Plupload</td>\n<td>Free</td>\n<td>YES!</td>\n</tr>\n</tbody>\n</table>\n<h2>Found a bug?</h2>\n<p>If you think you have found a bug please create an issue on the <a href=\"https://github.com/tinymce/tinymce/issues\">GitHub repo</a> to report it to the developers.</p>\n<h2>Finally ...</h2>\n<p>Don't forget to check out our other product <a href=\"http://www.plupload.com\" target=\"_blank\" rel=\"noopener\">Plupload</a>, your ultimate upload solution featuring HTML5 upload support.</p>\n<p>Thanks for supporting TinyMCE! We hope it helps you and your users create great content.<br />All the best from the TinyMCE team.</p>",
                DateCreated = DateTime.ParseExact("2013-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                DateLastEdited = DateTime.ParseExact("2013-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };
            context.Posts.Add(post1);

            var post2 = new Post
            {
                Id = Guid.Parse("e5954cec-dbc3-4d0b-b85a-5f4a253ff48b"),
                UserId = Guid.Parse(identityUser2.Id),
                Title = "Seed Post 2: Tiny MCE Basic Example",
                Slug = "seed-post-tiny-mce-basic-example",
                Content = "<p><img style=\"display: block; margin-left: auto; margin-right: auto;\" title=\"Tiny Logo\" src=\"https://www.tiny.cloud/images/illustrations/spot/tiny/illustration-spot-tiny-editor.svg\" alt =\"TinyMCE Logo\" width=\"128\" height=\"128\" /></p>\n<h2 style=\"text-align: center;\">Welcome to the TinyMCE editor demo!</h2>\n<h2>Got questions or need help?</h2>\n<ul>\n<li>Our <a href=\"https://www.tiny.cloud/docs/\">documentation</a> is a great resource for learning how to configure TinyMCE.</li>\n<li>Have a specific question? Try the <a href=\"https://stackoverflow.com/questions/tagged/tinymce\" target=\"_blank\" rel=\"noopener\"><code>tinymce</code> tag at Stack Overflow</a>.</li>\n<li>We also offer enterprise grade support as part of <a href=\"https://www.tiny.cloud/pricing\">TinyMCE premium plans</a>.</li>\n</ul>\n<h2>A simple table to play with</h2>\n<table style=\"border-collapse: collapse; width: 100%;\" border=\"1\">\n<thead>\n<tr>\n<th>Product</th>\n<th>Cost</th>\n<th>Really?</th>\n</tr>\n</thead>\n<tbody>\n<tr>\n<td>TinyMCE</td>\n<td>Free</td>\n<td>YES!</td>\n</tr>\n<tr>\n<td>Plupload</td>\n<td>Free</td>\n<td>YES!</td>\n</tr>\n</tbody>\n</table>\n<h2>Found a bug?</h2>\n<p>If you think you have found a bug please create an issue on the <a href=\"https://github.com/tinymce/tinymce/issues\">GitHub repo</a> to report it to the developers.</p>\n<h2>Finally ...</h2>\n<p>Don't forget to check out our other product <a href=\"http://www.plupload.com\" target=\"_blank\" rel=\"noopener\">Plupload</a>, your ultimate upload solution featuring HTML5 upload support.</p>\n<p>Thanks for supporting TinyMCE! We hope it helps you and your users create great content.<br />All the best from the TinyMCE team.</p>",
                DateCreated = DateTime.ParseExact("2021-12-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                DateLastEdited = DateTime.ParseExact("2021-12-02", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };
            context.Posts.Add(post2);

            context.PostRatings.Add(new PostRating
            {
                PostId = post1.Id,
                UserId = Guid.Parse(identityUser2.Id),
                Value = 2,
            });

            context.PostRatings.Add(new PostRating
            {
                PostId = post1.Id,
                UserId = Guid.Parse(identityUser1.Id),
                Value = 5,
            });

            var comment1 = new Comment
            {
                Id = Guid.Parse("66052591-2f42-4092-9330-7bb115109fc9"),
                UserId = Guid.Parse(identityUser1.Id),
                PostId = post1.Id,
                ParentCommentId = null,
                Content = "<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab</p>",
                DateCreated = DateTime.ParseExact("2013-01-02", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                DateLastEdited = DateTime.ParseExact("2013-01-02", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };
            context.Comments.Add(comment1);

            var comment2 = new Comment
            {
                Id = Guid.Parse("837e330d-f0b2-444d-bdbd-63c729aa7ee5"),
                UserId = Guid.Parse(identityUser2.Id),
                PostId = post1.Id,
                ParentCommentId = null,
                Content = "<p>Noice!</p><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud</p>",
                DateCreated = DateTime.ParseExact("2020-12-13", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                DateLastEdited = DateTime.ParseExact("2020-12-13", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };
            context.Comments.Add(comment2);

            var comment3 = new Comment
            {
                Id = Guid.Parse("688aed07-47a5-4ce5-9eec-2be0de5b2127"),
                UserId = Guid.Parse(identityUser2.Id),
                PostId = post1.Id,
                ParentCommentId = comment2.Id,
                Content = "<p>At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas</p>",
                DateCreated = DateTime.ParseExact("2020-12-14", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                DateLastEdited = DateTime.ParseExact("2020-12-14", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };
            context.Comments.Add(comment3);

            var comment4 = new Comment
            {
                Id = Guid.Parse("b1464a68-8976-4147-8d7e-f0feb3eb0d77"),
                UserId = Guid.Parse(identityUser1.Id),
                PostId = post1.Id,
                ParentCommentId = comment2.Id,
                Content = "<p>On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment</p>",
                DateCreated = DateTime.ParseExact("2020-12-14", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                DateLastEdited = DateTime.ParseExact("2020-12-14", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };
            context.Comments.Add(comment4);

            var comment5 = new Comment
            {
                Id = Guid.Parse("20ac1f20-b0cf-48ad-83bf-7e2fa1805ab6"),
                UserId = Guid.Parse(identityUser2.Id),
                PostId = post1.Id,
                ParentCommentId = comment4.Id,
                Content = "<p>he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.</p><p>God's in his heaven; all's right with the world.</p>",
                DateCreated = DateTime.ParseExact("2020-12-15", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                DateLastEdited = DateTime.ParseExact("2020-12-15", "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };
            context.Comments.Add(comment5);

            context.CommentStars.Add(new CommentStar
            {
                UserId = Guid.Parse(identityUser1.Id),
                CommentId = comment1.Id,
            });

            context.CommentStars.Add(new CommentStar
            {
                UserId = Guid.Parse(identityUser1.Id),
                CommentId = comment5.Id,
            });

            context.CommentStars.Add(new CommentStar
            {
                UserId = Guid.Parse(identityUser2.Id),
                CommentId = comment5.Id,
            });

            await context.SaveChangesAsync();
        }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace Makaan.BL.ViewModels.User;
public class RegisterVM
{
    [Required, MaxLength(64)]
    public string FullName { get; set; } = null!;

    [Required, MaxLength(32)]
    public string Username { get; set; } = null!;

    [Required, MaxLength(128), EmailAddress]
    public string Email { get; set; } = null!;

    [Required, MaxLength(32), DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required, MaxLength(32), DataType(DataType.Password), Compare(nameof(Password))]
    public string RePassword { get; set; } = null!;
}

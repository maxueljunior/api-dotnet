﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppSemTemplate.Services;

public class ImageUploadService : IImageUploadService
{
    public ImageUploadService()
    {
        
    }

    public async Task<bool> UploadArquivo(ModelStateDictionary modelState, IFormFile arquivo, string imgPrefixo)
    {
        if (arquivo.Length <= 0) return false;

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + arquivo.FileName);

        if (File.Exists(path))
        {
            modelState.AddModelError(string.Empty, "Já existe um arquivo com esse nome!");
            return false;
        }

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        return true;
    }
}

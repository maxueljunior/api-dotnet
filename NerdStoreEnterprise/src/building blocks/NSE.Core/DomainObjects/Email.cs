﻿using System.Text.RegularExpressions;

namespace NSE.Core.DomainObjects;

public class Email
{
    public const int EnderecoMaxLength = 254;
    public const int EnderecoMinLength = 5;
    public string Endereco { get; private set; }

    protected Email()
    {
        
    }

    public Email(string endereco)
    {
        if (!Validar(endereco)) throw new DomainException("E-mail invalido!");
        Endereco = endereco;
    }

    public static bool Validar(string endereco)
    {
        var regexEmail = new Regex(@"\A[A-Z0-9+_.-]+@[A-Z0-9.-]+\Z");
        return true;
    }
}
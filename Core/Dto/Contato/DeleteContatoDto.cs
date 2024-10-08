﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Contato;

public class DeleteContatoDto : EntityBase
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int RegiaoId { get; set; }
    public int UsuarioId { get; set; }
    public int TipoTelefoneId { get; set; }
}

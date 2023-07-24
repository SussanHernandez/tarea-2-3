﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEje2_3.Models
{
    public class Fotografia
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public byte[] Imagen { get; set; }
        public string Descripcion { get; set; }
    }
}

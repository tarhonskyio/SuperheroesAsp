﻿using System;
using System.Collections.Generic;

namespace SuperheroesAspNet.Models.Superheroes;

public partial class Colour
{
    public int Id { get; set; }

    public string? Colour1 { get; set; }

    public virtual ICollection<Superhero> SuperheroEyeColours { get; set; } = new List<Superhero>();

    public virtual ICollection<Superhero> SuperheroHairColours { get; set; } = new List<Superhero>();

    public virtual ICollection<Superhero> SuperheroSkinColours { get; set; } = new List<Superhero>();
}

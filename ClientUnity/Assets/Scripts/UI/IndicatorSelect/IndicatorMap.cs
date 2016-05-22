using System.Collections.Generic;
using System;

public class IndicatorNoda
{
    public IndicatorNoda Parent;    
    public List<IndicatorNoda> Child;
    public IndicatorNoda()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id;    
    public string Name;    
    public bool DefaultState;
}

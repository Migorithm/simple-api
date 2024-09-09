using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models;


[Table("Portfolio")]
public class Portfolio
{

    // fk to app user
    //! Entityframework will automatically recognize this as a foreign key
    public int AppUserId { get; set; }

    // fk to stock
    //! Entityframework will automatically recognize this as a foreign key
    public int StockId { get; set; }

    public AppUser AppUser { get; set; }
    public Stock Stock { get; set; }

}

# AutoTeile Shop Routen

## User Register:
Post ("")
[HttpGet("CheckCode/{mail}/{code}")]

## Product
HttpGet("")
HttpGet("/{id}")]
HttpGet("/ByName/{name}")
[HttpGet("/")] ?catagory
HttpPost("")
HttpPut("")

## Catagory
HttpGet("")
HttpGet("/{id}")
HttpGet("/ByName/{name}")
HttpGet("/{id}/Description")
HttpGet("") ? Type
HttpGet("") ? TopCatagry
HttpPost("")
HttpPut("")
HttpDelete("/{id}")

## User
HttpPut("/{guid}")
HttpDelete("/{guid}")
HttpGet("/{guid}")
[HttpGet("")

## ShoppingCart:
HttpGet("")
HttpGet("/{guid}")
HttpGet("/ByUser") ? User
HttpPost("")

## ShoppingCartItem
HttpGet("")
HttpGet("/{guid}")
HttpGet("/ShoppingCart") ? ShoppingCart
HttpPost("")
HttpPut("")
HttpDelete("")

## Car

Route("api/[controller]"
HttpGet("")
HttpGet("{id}"
HttpGet("GetByBaujahr/{year}")
HttpGet("GetByMarke/{marke}"
HttpGet("GetByModell/{model}")
HttpGet("GetByMarkeAndModell/{marke}/{model}")
HttpGet("GetByMarkeAndModellAndBaujahr/{merke}/{model}/{baujahr}")
HttpDelete("{id}")
HttpPost("")
HttpPut()

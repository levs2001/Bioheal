# Demo project for exploring json parsers for c#

Used json.NET library.Json file is stored in Assets/resources.

## TBD

Start() unity method is called somewhat simulteniously so I cant place json parsing in such method of some
manager class. At the moment, Circle class parses json himself and stores speed value in some field. As json
grows larger this solution would get really slow.  

# XTrim Calculator


This console application expects instructions for calculations. The instructions are in the format of `operation value`. 

The possible operations are:
* Add
* Subtract
* Multiply
* Delete 
* Apply

There can be any number of instructions, but there can be only one `Apply` instruction which has to be the last one. 

For example: 
```
[Input]
add 2
multiply 5
apply 3
```

The calculation takes seed from the `apply` instruction and applies each instruction in the order they appear from the top. It ignores any mathematical precedence. 

The above example will be executed as follows:


`(3 + 2) * 5 = 25`

The instructions can be saved in a text file which can be passed as argument to the application. 

`XTrimCalculator.ConsoleApp C:\Temp\instruction.txt`

The instructions can also be passed as in-line arguments

`XTrimCalculator.ConsoleApp "add 2" "mulitply 5" "apply 3"`

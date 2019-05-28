# README #

### Canonical equation transformation

(https://drive.google.com/file/d/0B-6ig9g8rtlMcFA2aGNpMzNKSV9lV3g0bzUyRW1UN2h4a1dR/view?usp=sharing)

Create an application for transforming equation into canonical form. An equation can be of any
order. It may contain any amount of variables and brackets.

The equation will be given in the following form:

    P1 + P2 + ... = ... + PN
    
where `P1..PN` - summands, which look like:

    ax^k
    
where 
  - `a` - floating point value;
  - `k` - integer value;
  - `x` - variable (each summand can have many variables).

For example:

  1. `"x^2 + 2xy + y^2 = 5y^2 - 3xy + 2y^2"` should be transformed into: `"-4x^2 - y^2 + 5xy = 0"`
  2. `"x - 2 = 1"` => `"x - 3 = 0"`
  3. `"x - (x^2 - xy - 2) = 0"` => `"-x^2 + xy + x + 2=0"`
  4. `"x - (x^0 - (0 - x + y - y^0)) = 0"` => `"y-2=0"`
  5. `"x^3 - y(x + (x-1)(y+3)) = 0"` => `"x^3-y^2x+y^2-4xy+3y=0"`

etc.

Should be transformed into:

x^2 - y^2 + 4.5xy = 0

It should be a C# console application with support of two modes of operation: `file` and `interactive`. 
    - In interactive mode application prompts user to enter equation and displays
result on enter. This is repeated until user presses Ctrl+C. 
    - In the file mode application
processes a file specified as parameter and writes the output into separate file with `.out`
extension. 

Input file contains lines with equations, each equation on separate line.

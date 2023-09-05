# AcuCbl_to_Arduino
A simple solution example to control your arduino from Cobol program.

# What is needed:
- An Arduino board.
- Nr. 3 Leds
- Nr. 3 220 Ohm Resistors 
- NET Framework 4.7.2
- AcuCobol-GT 7.0.0

# FAQ:
# 1. Why is this Cobol example not written using the Graphical features of AcuCobol?
- Because I want to keep it simple as much I can. And I don't want to use the 
  extended synthax. I want the most compatibility with most of the COBOL dialects,
  so, if you have a different COBOL compiler, the changes are minimal.
  And this sample is not to show the Graphical potentias of cobol.

# 2. What is the "IGP_SimpleSerial" executable file?
- As AcuCobol does not support natively serial communication, I wrote a small
  serial driver in C#.  
  I am simply calling it via C$SYSTEM COBOL routine.
  Now this serial can only write data. But I am thinking to improve it into
  the future. For Arduino I am sending a simple request like "1 15": it means
  the command "1" (turn on something), and keep it on for "15" seconds.
  This is possible to handle via powershell, but needs always to confirm admin rights... So that's why I prefer to write a small serial handler.
  
# 3. What is inside the Arduino ".ino" file?
- A simple example that waits an input from a serial communication, and turning on a 
  led depending on the choice entered into the cobol program.
  Leds are connected to pins: 13, 8, 11.
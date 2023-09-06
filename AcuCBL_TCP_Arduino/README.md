# AcuCbl_READ_Arduino
A simple solution showing how to read Arduino's Serial Port

# What is needed:
- An Arduino board.
- A sensor (Like MQx gas sensors)
- NET Framework 4.7.2
- AcuCobol-GT 7.0.0

# FAQ:
# 1. Why is this Cobol example not written using the Graphical features of AcuCobol?
- Because I want to keep it simple. 
  I am not showing the graphics power of AcuCobol, but how to obtain data
  from a serial communication.

# 2. What is the "SerialDriver" DLL?
- As AcuCobol does not support natively serial communication, I wrote a small
  serial driver in C#.  
  I am simply calling it via C$SYSTEM COBOL routine.
  
# 3. What is inside the Arduino ".ino" file?
- The Sketch for your aruino board.

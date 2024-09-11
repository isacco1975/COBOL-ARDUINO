# AcuCbl_TCP_Arduino
A simple solution showing how to read Arduino's Serial Port and Send data too.

# What is needed:
- An Arduino board, some components: I used LCD display, a LED and a GAS MQx sensor.
- NET Framework 4.7.2
- AcuCobol-GT 7.0.0

# FAQ:
# 1. Why is this Cobol example not written using the Graphical features of AcuCobol?
- Because I want to keep it simple. 
  I am not showing the graphics power of AcuCobol, but how to obtain/send data
  from/to a serial communication.

# 2. What is the "TcpSerialDriver"?
- As AcuCobol does not support natively serial communication, I wrote a small
  serial TCP Server to read/send data on Serial Communications in VB.  
  I am accessing it thru C%SOCKET routine.
  
# 3. What is inside the Arduino ".ino" file?
- The Sketch for your aruino board.

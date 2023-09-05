# CobolGarden
A simple solution in AcuCobol to control your water pump or anything else

- Usage: Change the value of these variables, recompile the CBL file.

       01 SETTINGS-GROUP.
      *-> This indicates how every days open the pump
          05 SETTINGS-NDAYS PIC  9(2)             VALUE 01.
      *-> This indicates how many seconds keep it running
          05 SETTINGS-NSECS PIC  9(2)             VALUE 15.

		I am planning to put these settings into a file, for future versions.

# FAQ:
# 1. Why is this Cobol example not written using the Graphical features of AcuCobol?
- Because I want to keep it simple as much I can. And I don't want to use the 
  extended synthax. I want the most compatibility with most of the COBOL dialects,
  so, if you have a different COBOL compiler, the changes are minimal.

# 2. What is the "IGP_SimpleSerial" executable file?
- As AcuCobol does not support natively serial communication, I wrote a small
  serial driver in C#. I am simply calling it via C$SYSTEM COBOL routine.
  Now this serial can only write data. But I am thinking to improve it into
  the future. For Arduino I am sending a simple request like "1 15": it means
  the command "1" (turn on something), and keep it on for "15" seconds.
  
# 3. What is inside the Arduino ".ino" file?
- A simple example that waits an input from a serial communication, and turning on a 
  led. 
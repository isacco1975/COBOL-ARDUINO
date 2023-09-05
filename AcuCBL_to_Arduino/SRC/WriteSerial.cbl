       IDENTIFICATION DIVISION.
       PROGRAM-ID.    "WriteSerial",  is initial.
       AUTHOR.        Isaac Garcia Peveri.
       REMARKS.       Written in AcuCobol 7.0.0
      *
       WORKING-STORAGE SECTION.
       77 KEY-PRESSED PIC X.
      *
       01 CMD-LINE.
          05 CMD-FUNC    PIC X(27)
             VALUE "start IGP_SimpleSerial.exe ".
          05 CMD-PARMS   PIC X(12)
      *     Serial parameters. Ending space is necessary!    
             VALUE "COM3 9600 8 ". 
      *     Arguments decoded by ARDUINO
          05 CMD-ARGS    PIC X(04)
             VALUE '1.05'.

      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       PROCEDURE DIVISION.
       MAIN.
            PERFORM DISPLAY-MENU

            PERFORM UNTIL KEY-PRESSED = "X" or "x"
               ACCEPT KEY-PRESSED

      *       Which led turn on? (1, 2, 3)?
               MOVE KEY-PRESSED    TO CMD-ARGS(1:1)
      *       For how many seconds?
               MOVE "05"           TO CMD-ARGS(3:2)
               CALL "C$SYSTEM"  USING CMD-LINE, 64

               PERFORM DISPLAY-MENU
            END-PERFORM

            STOP RUN
            .

      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       DISPLAY-MENU.
            DISPLAY WINDOW ERASE
            DISPLAY " *********************************"
            DISPLAY "  SENDING MESSAGE TO SERIAL PORT *"
            DISPLAY "  2023 ISAAC GARCIA PEVERI       *"
            DISPLAY " *********************************"
            DISPLAY " "
            DISPLAY "  ENTER 1 - Turn ON Green LED"
            DISPLAY "  ENTER 2 - Turn ON Red LED"
            DISPLAY "  ENTER 3 - Turn ON Yellow LED"
            DISPLAY "  PRESS X or x - TO EXIT"
            DISPLAY " "
            .
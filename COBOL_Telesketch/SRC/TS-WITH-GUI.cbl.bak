       IDENTIFICATION DIVISION.
       PROGRAM-ID.    "TS",  is initial.
       AUTHOR.        Isaac Garcia Peveri.
       REMARKS.       Written in AcuCobol 7.0.0
      *LAST-EDIT.     2024, SEPTEMBER 20.
      /
      ******************************************************************
      * A telesketch (etch a sketch) written in Cobol                  *
      * GUI VERSION                                                    *
      ******************************************************************
      *
       WORKING-STORAGE SECTION.
       77 KEY-PRESSED PIC X.
       77 SOCKET-HANDLE  USAGE HANDLE.
      *   Arguments decoded by ARDUINO
       77 CMD-ARGS       PIC X(10)      VALUE SPACES.
       77 X-VAL          PIC 9(4)       VALUE ZERO.
       77 Y-VAL          PIC 9(4)       VALUE ZERO.
       77 X-COL          PIC 9(4)       VALUE ZERO.
       77 Y-COL          PIC 9(4)       VALUE ZERO.
       77 W-COLOR        PIC 9(1)       VALUE 1.
       77 FORM1-H        USAGE HANDLE OF WINDOW.
       77 FORM2-H        USAGE HANDLE OF WINDOW.
       77 W-PIX          PIC X(8)       VALUE SPACES.
       77 W-PIX-N        PIC 9(8)       VALUE ZERO.
       77 IDX-C          PIC 9(4)       VALUE ZERO.
       77 IDX-R          PIC 9(4)       VALUE ZERO.
       77 Z-PIX          PIC ZZZZZZZZ   BLANK WHEN ZERO.
      *
          COPY "ACUGUI.DEF".
          COPY "ACUCOBOL.DEF".
          COPY "CRTVARS.DEF".
      *
       SCREEN SECTION.
       01 FORM1.
          05 LABEL LINE 3 COL 4 COLOR 3 HIGHLIGHT
             TITLE "Telesketch by IGP Tech Blog".

          05 X-LABEL LABEL LINE 4 COL 5 COLOR 3 HIGHLIGHT
             TITLE "X: ".

          05 X-LABEL-VALUE LABEL LINE 4 COL 8 COLOR 5 HIGHLIGHT
             TITLE " ".

          05 Y-LABEL LABEL LINE 4 COL 15 COLOR 3 HIGHLIGHT
             TITLE "Y: ".

          05 Y-LABEL-VALUE LABEL LINE 4 COL 19 COLOR 5 HIGHLIGHT
             TITLE " ".

          05 WPIX-LABEL-VALUE LABEL LINE 5 COL 5 COLOR 6 HIGHLIGHT
             TITLE " ".
      /
      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
      /
      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       PROCEDURE DIVISION.
       MAIN.
            DISPLAY STANDARD GRAPHICAL WINDOW
                    COLOR 0
                    SYSTEM MENU
                    TITLE "TELESKETCH"
                    HANDLE FORM2-H

            PERFORM INIT-AREA

            CALL 'C$SOCKET'         USING 3
                                          64000
                                          "127.0.0.1"
                                    GIVING SOCKET-HANDLE

            PERFORM WORKING-CYCLE

            STOP RUN
            .
      /
      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       INIT-AREA.
            DISPLAY FLOATING GRAPHICAL WINDOW MODAL
                    COLOR 1 HANDLE FORM1-H

            DISPLAY FORM1
            .
      /
      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       WORKING-CYCLE.
            PERFORM UNTIL 1 = 2
               CALL "C$SLEEP"       USING 0.01

               CALL 'C$SOCKET'      USING 6
                                    SOCKET-HANDLE
                                    CMD-ARGS
                                    9

               IF CMD-ARGS(1:4) NUMERIC AND CMD-ARGS(6:4) NUMERIC
                  UNSTRING CMD-ARGS DELIMITED BY ';'
                      INTO X-VAL
                  UNSTRING CMD-ARGS DELIMITED BY ' '
                      INTO Y-VAL

                  COMPUTE X-COL = X-VAL
                  COMPUTE Y-COL = Y-VAL

                  MODIFY X-LABEL-VALUE TITLE X-COL
                  MODIFY Y-LABEL-VALUE TITLE Y-COL

                  IF Y-COL < 6   MOVE 6 TO Y-COL   END-IF
                  IF Y-COL > 25  MOVE 25 TO Y-COL  END-IF

                  MOVE CMD-ARGS(6:4) TO W-PIX
                  MOVE CMD-ARGS(1:4) TO W-PIX(5:)
                  MODIFY WPIX-LABEL-VALUE    TITLE W-PIX
                  MOVE W-PIX TO W-PIX-N

                  DISPLAY FRAME  LINES = 0.5 SIZE = 0.5 TITLE = ""
                                 COLOR W-COLOR  HIGHLIGHT
                           AT    W-PIX-N PIXELS
               ELSE
                  IF CMD-ARGS(1:1) = 'C'
                     PERFORM ERASE-ENTIRE-SCREEN
                  END-IF

                  IF CMD-ARGS(1:1) = 'R'
                     MOVE 5 TO W-COLOR
                  END-IF

                  IF CMD-ARGS(1:1) = 'B'
                     MOVE 2 TO W-COLOR
                  END-IF

                  IF CMD-ARGS(1:1) = 'Y'
                     MOVE 6 TO W-COLOR
                  END-IF

                  IF CMD-ARGS(1:1) = 'W'
                     MOVE 1 TO W-COLOR
                  END-IF
               END-IF
            END-PERFORM
            .
      /
      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       ERASE-ENTIRE-SCREEN.
            DESTROY FORM1
            PERFORM INIT-AREA
            .
      /
       END-PROGRAM. "TS".
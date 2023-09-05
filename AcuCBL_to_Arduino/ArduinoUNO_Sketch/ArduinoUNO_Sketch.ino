String serialValue = "";
String serialCmd = "";
String numSecs = "";

void setup() {
  pinMode(13, OUTPUT);
  pinMode(8, OUTPUT);
  pinMode(11, OUTPUT);

  Serial.begin(9600,SERIAL_8N1); // opens serial port, sets data rate to 9600 bps
}

void loop() {
  // send data only when you receive data:
  if (Serial.available()) 
  {
    serialValue = Serial.readString();
    serialCmd = serialValue.substring(0, 1);
    numSecs = serialValue.substring(2,4);

    if (serialCmd == "1")
    {
       digitalWrite(13, HIGH);     
    }
    if (serialCmd == "2")
    {
       digitalWrite(8, HIGH);     
    }
    if (serialCmd == "3")
    {
       digitalWrite(11, HIGH);     
    }
 
    delay(numSecs.toInt() * 1000);
  }

  digitalWrite(13, LOW);
  digitalWrite(8, LOW);
  digitalWrite(11, LOW);
}

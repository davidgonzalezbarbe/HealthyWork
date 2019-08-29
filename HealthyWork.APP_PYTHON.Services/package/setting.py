class setting(object):
    _USERNAME = ""
    _PASSWORD = ""
    _URL_BASE = ""

    _LOGIN_URL = ""
    _GET_PARAMETER = ""
    _SET_VALUES = ""

    humidityTimer = 30
    noiseTimer = 30
    temperatureTimer = 30
    
    def init(self,timer_humidity, timer_noise,timer_temperature):
        self.humidityTimer = timer_humidity
        self.noiseTimer = timer_noise
        self.temperatureTimer = timer_temperature

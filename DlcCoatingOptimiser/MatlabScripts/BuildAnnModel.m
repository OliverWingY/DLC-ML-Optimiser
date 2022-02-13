function [successfull] = BuildAnnModel()
successfull = false;

DepositionTime = [45.12278754;	36.0433069;	45.14334973;	47.50963872;	40.07774017;	37.10612236;	46.11791178;	42.2766028;	50.09629298;	41.1563868];
MicrowavePower = [900;	1050;	900;	1050;	1200;	1200;	1200;	900;	900;	1050];
WorkingPressure = [0.013;	0.011;	0.009;	0.009;	0.009;	0.011;	0.013;	0.013;	0.011;	0.013];
GasFlowRateRatio = [100;	100;	100;	10;	73.9;	10;	100;	10;	55;	55];
Hardness = [4.9;	4.1;	3.4;	2.1;	3.9;	2.5;	4.2;	2.2;	3.3; 4.7];

inputMatrix = [DepositionTime, MicrowavePower, WorkingPressure, GasFlowRateRatio].';

netconf = [6, 6];

AnnModel = feedforwardnet(netconf);
AnnModel = train(AnnModel, inputMatrix, Hardness.');

assignin('base', 'AnnModel', AnnModel)
successfull = true;
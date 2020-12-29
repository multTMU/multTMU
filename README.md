Multiplicity Total Measurement Uncertainty Estimator 

Application: Mult_TMU v. 1.0

Author:	Robert McElroy Jr
		mcelroyrd@ornl.gov

Install:
Tested and developed using Windows 10 only. 
Copy installer directory "Mult_TMU_app" to the C-drive. Note: the program relies on hardcoded full paths to the C-drive.
Run "setup.exe".
Move the directory "Multiplicity_TMU" that contains data and example data into the C-drive.

Build:
Requires Visual Studio with Visual Basic Workload. Developed in Visual Studio 2019 and not tested in other versions.

Description:
 
The neutron multiplicity counting measurement characterizes special nuclear material (SNM) by simultaneously obtaining estimates of the plutonium mass, leakage multiplication, and alpha-ratio. Uncertainty estimates of characterized SNM (e.g. mass of Pu) are critical to the safeguards and nuclear nonproliferation missions to compare declared and measured quantities of SNM. A tool is needed to reliably account for the numerous sources of measurement and model uncertainties.  

This software quantifies the uncertainty of the SNM characterization using a comprehensive source of random and systematic uncertainties in the measurement and model data. The SNM characterization is obtained using the point model of neutron multiplicity counting. The point model is fast, well established, and simple/robust enough, compared to methods such as Monte Carlo or deterministic transport, to characterize SNM without the large the phase-space (e.g. group nuclear data and assayed object geometry) that is impractical/impossible to know in the infield demands of safeguard measurements.  

The software quickly returns an estimate of uncertainties of the characterized SNM when provided a neutron multiplicity counting measurement file and estimates of the uncertainties in the measurement, such as item isotopic composition and nuclear data (e.g. the average number of neutrons released in fission). The calculated uncertainties can be compared to the uncertainties of a declared SNM characterization.  

This software is intended as an aide in the evaluation of the total measurement uncertainty (TMU) in thermal neutron multiplicity analysis of Pu-oxide and MOX materials that are typically encountered in safeguards applications. The estimated uncertainties are dependent on the quality of the user input data and the base assumptions of the estimates. The uncertainty estimates are principally based on simulations and measurements of the PSMC and ENMC counters.  

Solution:

The software is a Visual Basic GUI and back-end that returns values and uncertainties of SNM characterization (assay results) for a given neutron multiplicity counting measurement file when provided a user provided model and estimates of uncertainty. The software can extract certain model parameters form the measurement file and provides data files for common measurement configurations (e.g. definitions for standard containers, standard nuclear data uncertainties, and detector efficiencies). The user can modify these parameters via the GUI or by creating their own parameter files based on provided templates. 

The user provides a measurement file either by filling a field or selection via a file browser. Once the file is validated an initial estimate of the assay results is provided. The calculation is immediate on a modern laptop or workstation. The tool also advises if there are potential discrepancies between the measurement file and the current model (the software retains the last defined model). 

The first results are reported from the measurement file. The final section of results utilizes the measurement file and user configuration of the measurement and model. The user can modify nuclear data, system parameters, and item information. The user can modify the information in section of the model by loading an existing file, modifying it in the GUI, or starting from the GUI. The model information can be saved as new file through the GUI. From these input parameters and a measurement file the total measurement uncertainty is calculated and is writable to file. 

Methods:

The software utilizes the well known point model to simulate neutron multiplicity counting measurements to obtain an assay (mass, multiplication, etc.) of SNM. The uncertainties in the reported assay are also generated using the point model. The software rolls up many sources of uncertainty to generate a total measurement uncertainty (TMU). For example, the detector efficiency is a parameter in the point model that has an uncertainty that affects the assay results/uncertainty. To arrive at an uncertainty of the efficiency requires considering many physical parameters, such as detector cavity type, dimensions, and placement of He3 tubes. From these physical parameters the software is able to generate a TMU based on similar inputs for other sources of uncertainty in the point model, such as the nuclear data and the item measured. 

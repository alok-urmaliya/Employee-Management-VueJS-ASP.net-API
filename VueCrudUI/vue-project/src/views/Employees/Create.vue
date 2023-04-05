<template>
    <div class="container mt-5">
        <div class="card">
            <div class="card-header">
                <h4>Add Employee</h4>
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <div v-if="savesuccess == true" class="alert alert-success" role="alert">
                        Employee Added Successfully
                    </div>
                </div>
                <div class="mb-3">
                    <label  for="">First Name<span class="text-danger">*</span></label>
                    <p v-if="this.errortext.First != ''" class="alert alert-warning">{{ this.errortext.First }}</p>
                    <!-- <input @onkeydown = "alphaonly({event.key})" type='text' v-model="model.employee.firstName" class="form-control"> -->
                    <input onkeydown="return /[a-zA-Z]/i.test(event.key)" type='text' v-model="model.employee.firstName" class="form-control">
                </div>
                <div class="mb-3">
                    <label for="">Last Name</label>
                    <input onkeydown="return /[a-zA-Z]/i.test(event.key)" type="text" v-model="model.employee.lastName" class="form-control">
                </div>
                <div class="mb-3">
                    <label for="" style="display : block">Gender</label>
                    <div class="btn-group" role="group" aria-label="Basic radio toggle button group">
                        <input type="radio" value="Male" v-model="this.model.employee.gender" class="btn-check"
                            name="genderradio" id="btnradio1" autocomplete="off">
                        <label class="btn btn-outline-primary" for="btnradio1">Male</label>

                        <input type="radio" value="Female" v-model="this.model.employee.gender" class="btn-check"
                            name="genderradio" id="btnradio2" autocomplete="off">
                        <label class="btn btn-outline-primary" for="btnradio2">Female</label>

                        <input type="radio" value="Other" v-model="this.model.employee.gender" class="btn-check"
                            name="genderradio" id="btnradio3" autocomplete="off">
                        <label class="btn btn-outline-primary" for="btnradio3">Other</label>
                    </div>
                </div>

                <div class="mb-3">
                    <label for="">City<span class="text-danger">*</span></label>
                    <p v-if="this.errortext.City != ''" class="alert alert-warning">{{ this.errortext.City }}</p>
                    <input onkeydown="return /[a-zA-Z]/i.test(event.key)" type="text" v-model="model.employee.city" class="form-control">
                </div>
                <div class="mb-3">
                    <button type="button" @click="SaveEmployee" class="btn btn-outline-primary">
                        Submit
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { stringifyExpression } from '@vue/compiler-core';
import axios from 'axios';
export default
    {
        name: "EmployeeCreate",
        data() {
            return {
                employee: '',
                savesuccess : false,
                errortext: {
                    First: '',
                    City: ''
                },
                count: 0,
                model: {

                    employee: {
                        firstName: '',
                        lastName: '',
                        gender: '',
                        city: '',
                        isActive: 'true'
                    }
                }
            }
        },
        methods: {

            SaveEmployee() {
                this.errortext = {
                    First: '',
                    City: '',
                }
                this.count = 0;
                if (this.model.employee.firstName == '') {
                    this.errortext.First = "First Name Cannot be Empty."
                    this.count = 1
                }
                if (this.model.employee.city == '') {
                    this.errortext.City = "City Cannot be Empty."
                    this.count = 2
                }
                console.log(this.model.employee);
                if (this.count == 0) {
                    axios.post('https://localhost:7130/api/Employees', this.model.employee)
                        .then(res =>
                            this.model.employee = {
                                firstName: '',
                                lastName: '',
                                gender: '',
                                city: '',
                                isActive: 'true'
                            },
                            this.savesuccess = true,
                            setTimeout(x => (this.savesuccess = false),1000),
                            window.location.href = '/employees'
                        )
                        .catch(function (error) {
                            if (error.response) {
                                if (error.response.status == 422) {

                                }
                                console.log(error.response.data);
                                console.log(error.response.status);
                                console.log(error.response.headers);
                            } else if (error.request) {
                                console.log(error.request);
                            } else {
                                console.log('Error', error.message);
                            }
                            console.log(error.config);
                        });
                }
            },
            // alphaonly(key) {
            //     console.log("key");
            // return /[a-zA-Z]/i.test(key);
            // }   
        }
    }
</script>
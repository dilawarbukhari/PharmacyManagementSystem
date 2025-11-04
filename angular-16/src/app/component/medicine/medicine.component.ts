import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { MedicineResponse } from 'src/app/Interface/Medicine';
import { MedicineRequest } from 'src/app/Interface/MedicineRequest';
import { CategoryService } from 'src/app/Services/category/category.service';
import { MedicineService } from 'src/app/Services/medicine/medicine.service';
import { SupplierService } from 'src/app/Services/supplier/supplier.service';
import { CommonService } from 'src/app/utilities/common.service';
declare var $: any;
@Component({
  selector: 'app-medicine',
  templateUrl: './medicine.component.html',
  styleUrls: ['./medicine.component.scss']
})
export class MedicineComponent {
  deleteText = '';
  isModelCheck=true;
  medicineId=0;
  medicineFormGroup!:FormGroup
    destroy$: Subject<void> = new Subject<void>();
  supplierId=0;
  medicineResponse:MedicineResponse
  medicineRequest:MedicineRequest;
  FilteredResponse:any[]=[];
  MedicineResponseList:any[]=[];
  CategoryResponse:any[]=[];
  SupplierResponse:any[]=[];

ngOnInit(){
  this.SetValidation();
  this.getMedicineDetails();
 this.getAllSupplier();
 this. GetCategory();
}


constructor(private _service:MedicineService,private _categoryservice :CategoryService,private _supplierservice: SupplierService,private fb:FormBuilder,private _commonService:CommonService){
this.medicineResponse=new MedicineResponse();
this.medicineRequest=new  MedicineRequest();
}
SetValidation(){
  this.medicineFormGroup=this.fb.group({
    medicineName:['',Validators.required],
    categoryId:['',Validators.required],
    supplierId:['',Validators.required],
    batchNumber:['',Validators.required],
    expiryDate:['',Validators.required],
    price:['',Validators.required],
    stockQuantity:['',Validators.required],
  })
}
  deleteDetails(){
  }
  GetCategory(){
    this._categoryservice.GetAllCategory().subscribe((response )=>{
    this.CategoryResponse=response.data;
      })
    }

    onreset(){
      this.medicineResponse.Name='';
      this.medicineResponse.supplierId=0;
      this.medicineResponse.categoryId=0;
      this.getMedicineDetails();

    }

   onSearch(){
      debugger
      this._service.GetAllMedicine(this.medicineResponse).subscribe((response)=>{
        this.FilteredResponse=response.data;
        this.MedicineResponseList=response.data;
      })
    }
    AddMedicine(){
      debugger
      if(this.medicineFormGroup.valid){
        this._service.AddMedicine(this.medicineFormGroup.value).subscribe((response)=>{
          this._commonService.showToast(response.message,'','success') 
          this.getMedicineDetails();
          $('#medicineModal').modal('hide');
       },
         (error)=>{        
             this._commonService.showToast(" Error Try Again", '', 'error')     
         })
      }
      this.medicineFormGroup.markAllAsTouched();
    }
getMedicineDetails(){
  debugger
  this._service.GetAllMedicine(this.medicineResponse).subscribe((response)=>{
    debugger
    this.FilteredResponse=response.data;
    this.MedicineResponseList=response.data;

  })
}
getAllSupplier(){
  this._supplierservice.GetAllSupplier().subscribe((response)=>{
    this.SupplierResponse=response.data;
  })
    }

  openCategoryModal() {
    this.medicineFormGroup.reset();
    this.isModelCheck=true;
    $('#medicineModal').modal('show');
  }
  isEditDetail(response:any){
    this.isModelCheck=false;
    $('#medicineModal').modal('show');
    debugger
    response.expiryDate = new Date(response.expiryDate).toISOString().split('T')[0];
    this.medicineFormGroup.patchValue(response);
    this.medicineId=response.medicineId;
    
  }

  updateDetails(){
    debugger
this.medicineRequest=this.medicineFormGroup.value;
this.medicineRequest.medicineId=this.medicineId;
    if(this.medicineFormGroup.valid){
      this._service.UpdateMedicine(this.medicineRequest).subscribe((response)=>{
        this._commonService.showToast(response.message,'','success') 
        this.getMedicineDetails();
        $('#medicineModal').modal('hide');
     },
       (error)=>{
        
           this._commonService.showToast(" Error Try Again", '', 'error')     
       })
    }
    this.medicineFormGroup.markAllAsTouched();
  
  }
  deleteCategory(){
    this._service.DeleteMedicine(this.medicineId).subscribe((response)=>{
      this._commonService.showToast(response.message,'','success') 
      this.getMedicineDetails();
      $('#deleteModal').modal('hide');
   },
     (error)=>{
      
         this._commonService.showToast(" Error Try Again", '', 'error')     
     })
  }

  openDeleteModal(response:any){
    $('#deleteModal').modal('show');
    this.medicineId=response.medicineId;
    this.deleteText=response.medicineName;
  }
}
